namespace WebRexErpAPI.BusinessServices.ShipEngine.Dto
{
    public class SECustoms
    {
        public string? contents { get; set; }
        public List<object>? customs_items { get; set; }
        public string? non_delivery { get; set; }
        public object? buyer_shipping_amount_paid { get; set; }
        public object? duties_paid { get; set; }
    }
}
