using WebRexErpAPI.Business.ShippingInformation;
using WebRexErpAPI.Business.ShippingInformation.Dto;
using WebRexErpAPI.BusinessServices.ShipEngine;
using WebRexErpAPI.BusinessServices.ShipEngine.Dto;
using WebRexErpAPI.Data.UnitOfWork;
using WebRexErpAPI.Helper;
using WebRexErpAPI.Models;
using Newtonsoft.Json;

namespace WebRexErpAPI.Business.Industry
{
    public class ShippingInformationService : IShippingInformationService,IDisposable
    {
        private readonly IUnitOfWork  _unitOfWork;
        private readonly IShipEngineService _shipEngineService;
        public ShippingInformationService(
            IUnitOfWork unitOfWork, IShipEngineService shipEngineService)
        {
            _unitOfWork = unitOfWork;
            _shipEngineService = shipEngineService;
        }

        public async Task<ShoppingInformation> SaveShippingInformationAsync(ShippingInformationDto input)
        {

            try
            {
                var validateAddress = new SEAddressDTO(){ 
                address_line1 = input.Address1+ " " + input.Address2,
                city_locality = input.City,
                postal_code = input.Zip_PostalCode,
                state_province = input.State,
                country_code = input.Country,
                };
               var jsonResponse = await _shipEngineService.ValidateAddressAsync(validateAddress);


                var response = JsonConvert.DeserializeObject<SEResponseAddress[]>(jsonResponse);
                if (response.Length > 0 && response[0].status == "verified")
                {
                    var ShippingInformationItem = input.MapSameProperties<WebRexErpAPI.Models.ShoppingInformation>();
                    if (ShippingInformationItem != null)
                    {
                        await _unitOfWork.shoppingInformationRepository.Add(ShippingInformationItem);
                        await _unitOfWork.CompleteAsync();
                    }

                    return ShippingInformationItem;
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

        public Task<List<ShippingInformationDto>> getUserShippings(int userId)
        {
            var res = new List<ShippingInformationDto>();
            var shippingInfo = _unitOfWork.shoppingInformationRepository.FindAll(a => a.UserId == userId).ToList();

            if(shippingInfo != null)
            {
                foreach (var shipItem in shippingInfo)
                {
                 var itemDto =   shipItem.MapSameProperties<ShippingInformationDto>();
                    res.Add(itemDto);
                }
              
            }

            return Task.FromResult(res);

        }

        public async Task UpdateShippingInformationAsync(ShippingInformationDto input)
        {

            var data = _unitOfWork.shoppingInformationRepository.FindAll(x => x.Id == input.Id).FirstOrDefault();
            if (data != null && data.Id > 0)
            {
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
                data.NameAlias = input.NameAlias;

                await _unitOfWork.shoppingInformationRepository.Update(data);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<bool> DeletShippingInformationAsync(int id)
        {
            var data = _unitOfWork.shoppingInformationRepository.FindAll(x => x.Id == id).FirstOrDefault();
            if(data != null && data.Id > 0)
            {
                await _unitOfWork.shoppingInformationRepository.Remove(data.Id);
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

