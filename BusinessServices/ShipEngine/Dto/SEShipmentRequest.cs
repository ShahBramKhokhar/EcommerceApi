namespace WebRexErpAPI.BusinessServices.ShipEngine.Dto
{
    public class SEShipmentRequest
    {
        public string? service_code { get; set; }
        public string? pickup_date { get; set; }
        public List<SEPackage>? packages { get; set; }
        public List<SEOptionValues>? optionValues { get; set; }
        public SERequestAddress? to_address { get; set; }
        public SERequestAddress? from_address { get; set; }
        public SEShipeEngineBill? bill { get; set; }
        public SERequestedBy? shipeEngineRequestedBy { get; set; }

    }
}
 