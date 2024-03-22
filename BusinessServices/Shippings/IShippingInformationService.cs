using WebRexErpAPI.Business.ShippingInformation.Dto;
using WebRexErpAPI.Models;

namespace WebRexErpAPI.Business.ShippingInformation
{
    public interface IShippingInformationService
    {
        Task<ShoppingInformation> SaveShippingInformationAsync(ShippingInformationDto input);
        Task UpdateShippingInformationAsync(ShippingInformationDto input);
        Task<bool> DeletShippingInformationAsync(int id);
        Task<List<ShippingInformationDto>> getUserShippings(int userId);

    }
}
