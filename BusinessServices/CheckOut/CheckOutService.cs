using Hangfire;
using WebRexErpAPI.Business.Email;
using WebRexErpAPI.BusinessServices.CheckOut.Dto;
using WebRexErpAPI.BusinessServices.Shippings.Dto;
using WebRexErpAPI.BusinessServices.Stripe.Dto;
using WebRexErpAPI.Common.CommonDto;
using WebRexErpAPI.Data.UnitOfWork;
using WebRexErpAPI.Services.Common;
using WebRexErpAPI.Services.QuickBase;
using Newtonsoft.Json;
using System.Text;

namespace WebRexErpAPI.Business.CheckOut
{
    public class CheckOutService : ICheckOutService ,IDisposable
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IQuickBaseService _quickBaseService;
        private readonly IEmailService _emailService;
        private readonly HttpClient _httpClient;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public CheckOutService(
            IUnitOfWork unitOfWork,
            IQuickBaseService quickBaseService,
            IEmailService emailService,
            IBackgroundJobClient backgroundJobClient

            )
        {
            _unitOfWork = unitOfWork;
            _quickBaseService= quickBaseService;
            _emailService= emailService;
            _httpClient = new HttpClient();
            _backgroundJobClient = backgroundJobClient;

        }

        public async Task<ResposeMessage> SaveCheckOutAsync(CheckoutInputModel input)
        {

            var paymentInfo = await findselectedPaymentPreferenceDetails(input);
            if (paymentInfo != null && paymentInfo.Id > 0)
            {

                var paymentDetail = await PaymentProcessing(input, paymentInfo);

                if (paymentDetail != null && paymentDetail.Status == "succeeded")
                {
                   //await SaveOrderInQuickbaseTables(input, paymentDetail, paymentInfo);
                   _backgroundJobClient.Enqueue(() => SaveOrderInQuickbaseTables(input,paymentDetail, paymentInfo));
                    return new ResposeMessage { Message = "Payment processed successfully", StatusCode = 200 };
                }

                else
                {
                    return new ResposeMessage { Message = "payment failed", StatusCode = 400 };

                }
            }
            else
            {
                return new ResposeMessage { Message = "check out process not working some wrong .", StatusCode = 400 };

            }

        }

        public async Task SaveOrderInQuickbaseTables(CheckoutInputModel input, PaymentIntentDto paymentDetail, Models.PaymentPreference paymentInfo)
        {

            ContactQBDto contactQBDto = new ContactQBDto()
            {
                ContactName = input.SelectedShippingOption.ContactName,
                ContactTitle = input.SelectedShippingOption.NameAlias,
                PhoneNumber = input.SelectedShippingOption.PhoneNumber,
                Email = input.SelectedShippingOption.Email,
            };
            var customerQBDto = await _quickBaseService.FindCustomerQBBusiness(input.UserDetail.BusinessName);

            contactQBDto.CustomerRecordId = customerQBDto.QBId;

            customerQBDto.CustomerLocation = new LocationQBDto();
            customerQBDto.CustomerLocation.Address = input.SelectedShippingOption.Address1;
            customerQBDto.CustomerLocation.Address2 = input.SelectedShippingOption.Address2;
            customerQBDto.CustomerLocation.City = input.SelectedShippingOption.City;
            customerQBDto.CustomerLocation.State = input.SelectedShippingOption.State;
            customerQBDto.CustomerLocation.PostalCode = input.SelectedShippingOption.Zip_PostalCode;
            customerQBDto.CustomerLocation.Country = input.SelectedShippingOption.Country;

            customerQBDto = await CheckAndQBCustomerCreation(input, contactQBDto, customerQBDto);

            contactQBDto.CustomerLocation = new LocationQBDto();
            contactQBDto.CustomerLocation.Address = input.SelectedShippingOption.Address1;
            contactQBDto.CustomerLocation.Address2 = input.SelectedShippingOption.Address2;
            contactQBDto.CustomerLocation.City = input.SelectedShippingOption.City;
            contactQBDto.CustomerLocation.State = input.SelectedShippingOption.State;
            contactQBDto.CustomerLocation.PostalCode = input.SelectedShippingOption.Zip_PostalCode;
            contactQBDto.CustomerLocation.Country = input.SelectedShippingOption.Country;

            var contact = await CheckAndQBContactCreation(contactQBDto);

            if (contact != null && contact.QBId > 0)
            {
                var QBCustomerLocation = await CheckAndQBCustomerLocationCreation(input, customerQBDto, contact);

                if (QBCustomerLocation != null && QBCustomerLocation.QBId > 0)
                {
                    QBOrderDto qBOrderDto = new QBOrderDto()
                    {
                        QBContactID = contact.QBId,
                        CustomerLocation = QBCustomerLocation,
                        date = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                        StripePaymnetId = "https://dashboard.stripe.com/search?query=" + paymentDetail.LatestChargeId,
                        TotalTax = input.TotalTax,
                    };

                    var order = await _quickBaseService.CreateQBOrder(qBOrderDto);

                    if (order != null && order.QBId > 0)
                    {

                        input.OrderNo = order.OrderNo;

                        input.PPDetail.Id = paymentInfo.Id;
                        input.PPDetail.PaymentMethodId = paymentInfo.PaymentMethodId;
                        input.PPDetail.CardType = paymentInfo.CardType;
                        input.PPDetail.CardLastDigits = paymentInfo.CardLastDigits;
                        input.PPDetail.NameAlias = paymentInfo.NameAlias;
                        input.PPDetail.UserId = paymentInfo.UserId;
                        input.PPDetail.ExpireDate = paymentInfo.ExpireDate;


                        input.StripePayment = paymentDetail.Amount;
                        await _emailService.OrderConfirmationEmail(input);

                        var packagingPrice = input.CartPackagingAmount / 2;
                        packagingPrice = packagingPrice / input.Items.Count();

                        foreach (var item in input.Items)
                        {
                            SO_ItemQBDto sO_ItemQBDto = new SO_ItemQBDto()
                            {
                                ItemRecordId = item.QbRecordId,
                                SelectedQty = item.SelectedQty,
                                OrderId = order.QBId,
                                Freight = item.SelectedFreightPrice,
                                Postal = item.SelectedPostalPrice,
                                Packing = packagingPrice,
                                ShippingType = item.SelectedShippingOption

                            };
                            var soItem = _quickBaseService.CreateQBSOItem(sO_ItemQBDto);
                        }

                        //QBChargeDto qBChargeDtoP = new QBChargeDto()
                        //{
                        //    OrderId = order.QBId,
                        //    Amout = input.CartPackagingAmount,
                        //    ChargeType = "Packaging",
                        //    PaymentType = "Credit Card Terminal (Bank Account)",
                        //};

                        //var chargeP = _quickBaseService.CreateQBCharge(qBChargeDtoP);
                        //var payment_P = _quickBaseService.CreateQBPayment(qBChargeDtoP);

                        //QBChargeDto qBChargeDtoF = new QBChargeDto()
                        //{
                        //    OrderId = order.QBId,
                        //    Amout = input.CartTotalShippingAmount,
                        //    ChargeType = "Freight",
                        //    PaymentType = "Credit Card Terminal (Bank Account)",
                        //};

                        //var chargF = _quickBaseService.CreateQBCharge(qBChargeDtoF);
                        //var PayementF = _quickBaseService.CreateQBPayment(qBChargeDtoF);



                        //QBChargeDto qBPaymentDtoInv = new QBChargeDto()
                        //{
                        //    OrderId = order.QBId,
                        //    Amout = input.CartTotalAmount,
                        //    ChargeType = "Inventory",
                        //    PaymentType = "Credit Card Terminal (Bank Account)",
                        //};


                        //QBChargeDto qBChargeDtoInv = new QBChargeDto()
                        //{
                        //    OrderId = order.QBId,
                        //    Amout = input.CartTotalAmount,
                        //    ChargeType = "Inventory Item",
                        //    PaymentType = "Credit Card Terminal (Bank Account)",
                        //};


                        //var chargInv = _quickBaseService.CreateQBCharge(qBChargeDtoInv);
                        //var PaymentInv = _quickBaseService.CreateQBPayment(qBPaymentDtoInv);

                    }

                }

            }




        }

        private async Task<LocationQBDto> CheckAndQBCustomerLocationCreation(CheckoutInputModel input, CustomerQBDto? customerQBDto, ContactQBDto contact)
        {
            LocationQBDto qbLocation = new LocationQBDto()
            {
                QBId = contact.QBId,
                Address = input.SelectedShippingOption.Address1,
                CustomerRecordId = customerQBDto.QBId,
                City = input.SelectedShippingOption.City,
                Country = input.SelectedShippingOption.Country,
                Name = input.SelectedShippingOption.NameAlias,
                PostalCode = input.SelectedShippingOption.Zip_PostalCode,
                State = input.SelectedShippingOption.State,

            };

            LocationQBDto QBCustomerLocation = new LocationQBDto();
            QBCustomerLocation = await _quickBaseService.FindQBCustomerLocation(qbLocation);

            if (QBCustomerLocation == null)
            {
                QBCustomerLocation = await _quickBaseService.CreateQBLocationCustomer(qbLocation);
            }

            return QBCustomerLocation;
        }

        private async Task<ContactQBDto> CheckAndQBContactCreation(ContactQBDto contactQBDto)
        {
            var contact = new ContactQBDto();
            contact = await _quickBaseService.FindCustomerContacts(contactQBDto);
            if (contact.QBId == 0)
            {
                contact = await _quickBaseService.CreateQBContact(contactQBDto);
            }
            else
            {
                contactQBDto.QBId = contact.QBId;
                contact = await _quickBaseService.CreateQBContact(contactQBDto);
            }

            return contact;
        }

        private async Task<CustomerQBDto?> CheckAndQBCustomerCreation(CheckoutInputModel input, ContactQBDto contactQBDto, CustomerQBDto? customerQBDto)
        {
            contactQBDto.IsCreateCustomer = true;
            CustomerQBDto qbCustomer = new CustomerQBDto()
            {
                phoneNumber = input.SelectedShippingOption.PhoneNumber,
                CustomerName = input.UserDetail.BusinessName,
            };

            if (customerQBDto == null || customerQBDto.QBId == 0)
            {
                customerQBDto = await _quickBaseService.CreateQbCustomer(qbCustomer);
            }
            else
            {
                customerQBDto.QBId = customerQBDto.QBId;
                customerQBDto = await _quickBaseService.CreateQbCustomer(customerQBDto);
            }

            return customerQBDto;
        }

        private async Task<Models.PaymentPreference?> findselectedPaymentPreferenceDetails(CheckoutInputModel input)
        {
            var data = new Models.PaymentPreference();
            if (input.SelectedPPOption != null && Convert.ToInt32(input.SelectedPPOption) > 0)
            {
                var ppId = Convert.ToInt32(input.SelectedPPOption);
                var res = await _unitOfWork.paymentPreferenceRepository.FindAllAsync(a => a.Id == ppId);
                data = res.FirstOrDefault();
                return data;
            }

            else
            {
                return data;
            }

        }

        private async Task<PaymentIntentDto?> PaymentProcessing(CheckoutInputModel input, Models.PaymentPreference? paymentsIfno)
        {


            var paymentProcess = new FunctionAppPaymentDto();
            paymentProcess.TotalAmount = input.TotalAmount;
            paymentProcess.ContactName = input.SelectedBillingOption.ContactName;
            paymentProcess.Email = input.SelectedBillingOption.Email;
            paymentProcess.PhoneNumber = input.SelectedBillingOption.PhoneNumber;
            paymentProcess.StripePaymentId = paymentsIfno.PaymentMethodId;
            paymentProcess.Items = input.Items;

            var paymentCheckOutStatus = await StripPaymentUsingFunctionAppAsync(paymentProcess);
            return paymentCheckOutStatus;
        }


        private async Task<PaymentIntentDto?> StripPaymentUsingFunctionAppAsync(FunctionAppPaymentDto input)
        {
            try
            {
                _httpClient.BaseAddress = new Uri("https://stripeazurefunction.azurewebsites.net/api/ConfirmPayment?");
              //  _httpClient.BaseAddress = new Uri("http://localhost:7048/api/ConfirmPayment");
              // _httpClient.BaseAddress = new Uri("https://stripetestmodeazurefunction.azurewebsites.net/api/ConfirmPayment?");
                var requestBody = JsonConvert.SerializeObject(input);
                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("ConfirmPayment", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var paymentIntentDto = JsonConvert.DeserializeObject<PaymentIntentDto>(responseContent);
                    return paymentIntentDto;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork?.Dispose();
                _httpClient?.Dispose();
            }
        }
    }
}
