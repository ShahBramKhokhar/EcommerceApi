

namespace WebRexErpAPI.BusinessServices.ShipEngine.Dto
{
    public class SETLTFreightResponse
    {
        public object? carrier_message { get; set; }
        public string? carrier_quote_id { get; set; }
        public List<SECharge>? charges { get; set; }
        public object? effective_date { get; set; }
        public int? estimated_delivery_days { get; set; }
        public object? expiration_date { get; set; }
        public string? pickup_date { get; set; }
        public string? quote_id { get; set; }
        public object? quote_type { get; set; }
        public SEService? service { get; set; }
        public SEShipmentResponse? shipment { get; set; }
    }
}
