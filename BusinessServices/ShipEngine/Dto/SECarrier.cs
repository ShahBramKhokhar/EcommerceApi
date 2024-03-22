namespace WebRexErpAPI.BusinessServices.ShipEngine.Dto
{
    public class SECarrier
    {
        public string carrier_id { get; set; }
        public string carrier_code { get; set; }
        public string account_number { get; set; }
        public bool requires_funded_amount { get; set; }
        public decimal balance { get; set; }
        public string nickname { get; set; }
        public string friendly_name { get; set; }
        public bool primary { get; set; }
        public bool has_multi_package_supporting_services { get; set; }
        public bool supports_label_messages { get; set; }

    }

    public class CarriersResponse
    {
        public List<SECarrier> Carriers { get; set; }
    }
}
