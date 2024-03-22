using WebRexErpAPI.Business.Order.Dto;

namespace WebRexErpAPI.Business.Order
{
    public interface IOrderService
    {
        Task SaveOrderAsync(OrderDto input);


    }
}
