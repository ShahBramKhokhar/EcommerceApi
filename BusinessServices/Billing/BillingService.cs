using WebRexErpAPI.Business.Billing;
using WebRexErpAPI.Business.Billing.Dto;
using WebRexErpAPI.BusinessServices.ShipEngine;
using WebRexErpAPI.BusinessServices.ShipEngine.Dto;
using WebRexErpAPI.Data.UnitOfWork;
using WebRexErpAPI.Helper;
using Newtonsoft.Json;

namespace WebRexErpAPI.Business.Industry
{
    public class BillingService : IBillingService , IDisposable
    {
        private readonly IUnitOfWork  _unitOfWork;
        private readonly IShipEngineService _shipEngineService;
        public BillingService(IUnitOfWork unitOfWork, IShipEngineService shipEngineService)
        {
         _unitOfWork = unitOfWork;
          _shipEngineService = shipEngineService;

        }

        public async Task<WebRexErpAPI.Models.Billing> SaveBillingAsync(BillingDto input)
        {
            try
            {
                var validateAddress = new SEAddressDTO()
                {
                    address_line1 = input.Address1 + " " + input.Address2,
                    city_locality = input.City,
                    postal_code = input.Zip_PostalCode,
                    state_province = input.State,
                    country_code = input.Country,
                };
                var jsonResponse = await _shipEngineService.ValidateAddressAsync(validateAddress);


                var response = JsonConvert.DeserializeObject<SEResponseAddress[]>(jsonResponse);
                if (response.Length > 0 && response[0].status == "verified")
                {
                    var BillingItem = input.MapSameProperties<WebRexErpAPI.Models.Billing>();
                    if (BillingItem != null)
                    {
                        await _unitOfWork.billingRepository.Add(BillingItem);
                        await _unitOfWork.CompleteAsync();
                    }

                    return BillingItem;
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

        public Task<List<BillingDto>> getUserBilling(int userId)
        {
            var res = new List<BillingDto>();
            var shippingInfo = _unitOfWork.billingRepository.FindAll(a => a.UserId == userId).ToList();

            if(shippingInfo != null)
            {
                foreach (var shipItem in shippingInfo)
                {
                 var itemDto =   shipItem.MapSameProperties<BillingDto>();
                    res.Add(itemDto);
                }
              
            }

            return Task.FromResult(res);

        }

        public async Task UpdateBillingAsync(BillingDto input)
        {

            var data = _unitOfWork.billingRepository.FindAll(x => x.Id == input.Id).FirstOrDefault();
            if (data != null && data.Id > 0)
            {
                data.NameAlias = input.NameAlias;
                data.CompanyName = input.CompanyName;
                data.ContactName = input.ContactName;
                data.Email = input.Email;
                data.PhoneNumber = input.PhoneNumber;
                data.Address1 = input.Address1;
                data.Address2 = input.Address2;
                data.City = input.City;
                data.State = input.State;
                data.Zip_PostalCode = input.Zip_PostalCode;
                data.Country = input.Country;
               

                await _unitOfWork.billingRepository.Update(data);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<bool> DeletBillingAsync(int id)
        {
            var data = _unitOfWork.billingRepository.FindAll(x => x.Id == id).FirstOrDefault();
            if(data != null && data.Id > 0)
            {
                await _unitOfWork.billingRepository.Remove(data.Id);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            else
            {
                return false;
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
                
            }
        }
    }
}

