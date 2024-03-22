using WebRexErpAPI.Business.CheckOut.Dto;
using WebRexErpAPI.BusinessServices.CheckOut.Dto;
using WebRexErpAPI.Common.CommonDto;

namespace WebRexErpAPI.Business.CheckOut
{
    public interface ICheckOutService
    {
        Task<ResposeMessage> SaveCheckOutAsync(CheckoutInputModel input);

    }
}
