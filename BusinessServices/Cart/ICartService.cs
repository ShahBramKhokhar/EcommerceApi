using WebRexErpAPI.Business.Industry.Dto;
using WebRexErpAPI.Business.Cart.Dto;

namespace WebRexErpAPI.Business.Cart
{
    public interface ICartService
    {
        Task SaveCartListAsync(List<CartDto> CartDtolist);
    }
}
