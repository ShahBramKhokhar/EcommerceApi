using WebRexErpAPI.Business.Address.Dto;

namespace WebRexErpAPI.Business.Address
{
    public interface IUserAddressService
    {
        Task SaveAddressAsync(UserAddressDto input);
        Task UpdateAddressAsync(UserAddressDto input);
        Task<bool> DeletAddressAsync(int id);
        Task<List<UserAddressDto>> getUserAddresses(int userId);

    }
}
