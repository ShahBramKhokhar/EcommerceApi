namespace WebRexErpAPI.Business.Order.Dto
{
    public class OrderDto
    {
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? OrderTotal { get; set; }
        public string? OrderId { get; set; }
        public int? TotalTax { get; set; }
        public int? TotalDiscount { get; set; }
        public int? ShipmentCharges { get; set; }
        public string? ShippingAddress { get; set; }
        public int? ShippingCharges { get; set; }
        public string? PaymentMathod { get; set; }
        public string? StripePaymentID { get; set; }
        public string? PayPalPaymentID { get; set; }
        public string? WireTransferRef { get; set; }
        public string? ShippingProvider { get; set; }
        public int? UserId { get; set; }
       

    }
}
