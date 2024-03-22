namespace WebRexErpAPI.BusinessServices.ShipEngine.Dto
{
    public class SERequestAddress
    {
        public string? name { get; set; }
        public string? company_name { get; set; }
        public string? address_line1 { get; set; }
        public string? address_line2 { get; set; }
        public string? city_locality { get; set; }  
        public string? state_province { get; set; }
        public string? postal_code { get; set; }
        public string? country_code { get; set; }
        public string? phone { get;  set; }
        public SEContact? shipeEngineContact { get; set; }

    }



}
