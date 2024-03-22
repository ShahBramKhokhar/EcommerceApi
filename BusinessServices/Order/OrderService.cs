using WebRexErpAPI.Business.Order;
using WebRexErpAPI.Business.Order.Dto;
using WebRexErpAPI.Data.UnitOfWork;
using WebRexErpAPI.Helper;

namespace WebRexErpAPI.Business.Order
{
    public class OrderService : IOrderService,IDisposable
    {

        private readonly IUnitOfWork  _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
         _unitOfWork = unitOfWork;

        }

        public async Task SaveOrderAsync(OrderDto input)
        {
            try
            {
                    var OrderItem = input.MapSameProperties<WebRexErpAPI.Models.Order>();
                    if (OrderItem != null)
                    {
                        await _unitOfWork.orderRepository.Add(OrderItem);
                        await _unitOfWork.CompleteAsync();
                    }

            }
            catch (Exception )
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
            }
        }
    }
}
