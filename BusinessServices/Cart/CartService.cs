using WebRexErpAPI.Business.Cart.Dto;
using WebRexErpAPI.Data.UnitOfWork;

namespace WebRexErpAPI.Business.Cart
{
    public class CartService : ICartService
    {

        private readonly IUnitOfWork  _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
         _unitOfWork = unitOfWork;

        }

        public async Task SaveCartListAsync(List<CartDto> CartDtolist)
        {

            try
            {
                foreach (var item in CartDtolist)
                {
                    var CartItem = new WebRexErpAPI.Models.Cart();
                    CartItem.ItemId = item.Id;
                    CartItem.Price = item.Price;
                    CartItem.Qty = (int?)item.Qty;
                    if (CartItem != null)
                    {
                        await _unitOfWork.cartRepository.Add(CartItem);
                        await _unitOfWork.CompleteAsync();
                    }

                }
            }
            catch (Exception )
            {
                throw;
            }

           
        }
    }
}
