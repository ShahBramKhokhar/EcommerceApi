using Microsoft.Extensions.Options;

namespace WebRexErpAPI.BusinessServices.ShipEngine.Dto
{
    public class SEShipmentResponse
    {
        public SEShipeEngineBill bill_to { get; set; }
        public List<SEOptionValues> options { get; set; }
        public List<SEPackage> packages { get; set; }
        public string pickup_date { get; set; }
        public SERequestedBy requested_by { get; set; }
        public string service_code { get; set; }
        public SEShipFrom ship_from { get; set; }
        public SEShipTo ship_to { get; set; }
    }
}
