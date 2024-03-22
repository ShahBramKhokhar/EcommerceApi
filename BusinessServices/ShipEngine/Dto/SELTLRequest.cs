namespace WebRexErpAPI.BusinessServices.ShipEngine.Dto
{
    public class SELTLRequest
    {
        public SEShipmentRequest? shipmentRequest { get; set; }
        public SEShipmentMeasurements? measurements { get; set; }

        public string? CarrierId { get; set; }
    }
}
