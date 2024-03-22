namespace WebRexErpAPI.BusinessServices.ShipEngine.Dto
{
    public class SERequest
    {
        public string? CarrierId { get; set; }
        public SERequestAddress? ToAddress { get; set; }
        public SERequestAddress? FromAddress { get; set; }
        public List<SEPackage>? packages { get; set; }
    }
}
