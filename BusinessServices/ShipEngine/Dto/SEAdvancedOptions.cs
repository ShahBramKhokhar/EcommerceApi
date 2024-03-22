namespace WebRexErpAPI.BusinessServices.ShipEngine.Dto
{
    public class SEAdvancedOptions
    {
        public object? bill_to_account { get; set; }
        public object? bill_to_country_code { get; set; }
        public object? bill_to_party { get; set; }
        public object? bill_to_postal_code { get; set; }
        public bool? contains_alcohol { get; set; }
        public bool? delivered_duty_paid { get; set; }
        public bool? non_machinable { get; set; }
        public bool? saturday_delivery { get; set; }
        public bool? dry_ice { get; set; }
        public object? dry_ice_weight { get; set; }
        public object? fedex_freight { get; set; }
        public bool? third_party_consignee { get; set; }
        public object? ancillary_endorsements_option { get; set; }
        public object? freight_class { get; set; }
        public object? custom_field1 { get; set; }
        public object? custom_field2 { get; set; }
        public object? custom_field3 { get; set; }
        public object? collect_on_delivery { get; set; }
        public object? return_pickup_attempts { get; set; }
        public bool? additional_handling { get; set; }
    }
}
