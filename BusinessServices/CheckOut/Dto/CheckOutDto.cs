using WebRexErpAPI.Business.Cart.Dto;
using WebRexErpAPI.Business.Order.Dto;
using WebRexErpAPI.Business.ShippingInformation.Dto;

namespace WebRexErpAPI.Business.CheckOut.Dto
{
    public class CheckOutDto
    {
        public List<CartDto>? CartList { get; set; }
        public ShippingInformationDto? ShippingInformation { get; set; }
        public OrderDto? order { get; set; }
    }
}
