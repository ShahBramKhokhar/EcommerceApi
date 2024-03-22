using WebRexErpAPI.Business.Billing.Dto;
using WebRexErpAPI.Business.Industry.Dto;

namespace WebRexErpAPI.BusinessServices.Shippings.Dto
{
    public class PaymentProcessDto
    {
        public decimal TotalAmount { get; set; }
        public BillingDto UserDetail { get; set; }
        public string StripePaymentId { get; set; }
        public IEnumerable<ItemDto> Items { get; set; }
        public string? PaymnetLink { get; set; }
    }

    public class FunctionAppPaymentDto
    {
        public decimal TotalAmount { get; set; }
        public string Email { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public string StripePaymentId { get; set; }
        public List<ItemDto> Items { get; set; }
    }
}
