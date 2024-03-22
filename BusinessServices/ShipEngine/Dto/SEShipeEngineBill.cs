namespace WebRexErpAPI.BusinessServices.ShipEngine.Dto
{
    public class SEShipeEngineBill
    {
        public string? type { get; set; }
        public string? payment_terms { get; set; }
        public string? account { get; set; }
        public SERequestAddress? address { get; set; }
        public SEContact? contact { get; set; }
    }
}
