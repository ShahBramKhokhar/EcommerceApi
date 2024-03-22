namespace WebRexErpAPI.BusinessServices.ShipEngine.Dto
{
    public class SEPackage
    {

        public string? package_code { get; set; }
        public int? freight_class { get; set; }
        public string? nmfc_code { get; set; }
        public string? description { get; set; }
        public Dimensions? dimensions { get; set; }
        public SEWeight? weight { get; set; }
        public int? quantity { get; set; }
        public bool stackable { get; set; }
        public bool hazardous_materials { get; set; }

    }
}
