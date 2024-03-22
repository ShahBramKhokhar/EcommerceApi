namespace WebRexErpAPI.BusinessServices.ShipEngine.Dto
{
    public class SEShipTo
    {
        public object? instructions { get; set; }
        public string? name { get; set; }
        public string? phone { get; set; }
        public object? company_name { get; set; }
        public string? address_line1 { get; set; }
        public object? address_line2 { get; set; }
        public object? address_line3 { get; set; }
        public string? city_locality { get; set; }
        public string? state_province { get; set; }
        public string? postal_code { get; set; }
        public string? country_code { get; set; }
        public string? address_residential_indicator { get; set; }

        public Object? account { get; set; }
        public SEResponseAddress? address { get; set; }
        public SEContact? contact { get; set; }
    }
}
