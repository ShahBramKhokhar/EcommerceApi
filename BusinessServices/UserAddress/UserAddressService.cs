using WebRexErpAPI.Business.Address;
using WebRexErpAPI.Business.Address.Dto;
using WebRexErpAPI.Data.UnitOfWork;
using WebRexErpAPI.Helper;

namespace WebRexErpAPI.Business.UserAddress
{
    public class UserAddressService : IUserAddressService,IDisposable
    {
        private readonly IUnitOfWork  _unitOfWork;
        public UserAddressService(IUnitOfWork unitOfWork)
        {
         _unitOfWork = unitOfWork;

        }

        public async Task SaveAddressAsync(UserAddressDto input)
        {

            try
            {
                var AddressItem = input.MapSameProperties<WebRexErpAPI.Models.UserAddress>();
                if (AddressItem != null)
                {
                    await _unitOfWork.userAddressRepositoy.Add(AddressItem);
                    await _unitOfWork.CompleteAsync();
                }

            }
            catch (Exception)
            {

                throw;
            }
 

        }

        public Task<List<UserAddressDto>> getUserAddresses(int userId)
        {
            var res = new List<UserAddressDto>();
            var shippingInfo = _unitOfWork.userAddressRepositoy.FindAll(a => a.UserId == userId).ToList();

            if(shippingInfo != null)
            {
                foreach (var shipItem in shippingInfo)
                {
                 var itemDto =   shipItem.MapSameProperties<UserAddressDto>();
                    res.Add(itemDto);
                }
              
            }

            return Task.FromResult(res);

        }

        public async Task UpdateAddressAsync(UserAddressDto input)
        {

            var data = _unitOfWork.userAddressRepositoy.FindAll(x => x.Id == input.Id).FirstOrDefault();
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

                await _unitOfWork.userAddressRepositoy.Update(data);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<bool> DeletAddressAsync(int id)
        {
            var data = _unitOfWork.userAddressRepositoy.FindAll(x => x.Id == id).FirstOrDefault();
            if(data != null && data.Id > 0)
            {
                await _unitOfWork.userAddressRepositoy.Remove(data.Id);
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

