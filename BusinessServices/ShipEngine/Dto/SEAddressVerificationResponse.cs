namespace WebRexErpAPI.BusinessServices.ShipEngine.Dto
{
    public class SEAddressVerificationResponse
    {
        public string status { get; set; }
        public SEResponseAddress original_address { get; set; }
        public SEResponseAddress matched_address { get; set; }
        public List<string> messages { get; set; }
    }
}
