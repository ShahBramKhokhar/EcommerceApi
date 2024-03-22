namespace WebRexErpAPI.BusinessServices.ShipEngine.Dto
{
    public class SEGetRateResponse
    {
       
        public SERateResponse? rate_response { get; set; }
        public string? shipment_id { get; set; }
        public string? carrier_id { get; set; }
        public object? service_code { get; set; }
        public object? external_shipment_id { get; set; }
        public object? shipment_number { get; set; }
        public DateTime? ship_date { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? modified_at { get; set; }
        public string? shipment_status { get; set; }
        public SEShipTo? ship_to { get; set; }
        public SEShipFrom? ship_from { get; set; }
        public object? warehouse_id { get; set; }
        public SEReturnTo? return_to { get; set; }
        public string? confirmation { get; set; }
        public SECustoms? customs { get; set; }
        public object? external_order_id { get; set; }
        public object? order_source_code { get; set; }
        public SEAdvancedOptions? advanced_options { get; set; }
        public string? insurance_provider { get; set; }
        public List<object>? tags { get; set; }
        public List<SEPackage>? packages { get; set; }
        public SETotalWeight? total_weight { get; set; }
        public List<object>? items { get; set; }
    }


}
