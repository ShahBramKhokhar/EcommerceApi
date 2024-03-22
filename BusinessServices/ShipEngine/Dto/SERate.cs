namespace WebRexErpAPI.BusinessServices.ShipEngine.Dto
{
    public class SERate
    {
        public string CarrierId { get; set; }
        public string ServiceName { get; set; }
        public string CarrierCode { get; set; }
        public string CarrierNickname { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceDescription { get; set; }
        public string Trackable { get; set; }
        public string EstimatedDeliveryDate { get; set; }
        public string DeliveryDays { get; set; }
        public decimal BaseRate { get; set; }
        public decimal CarrierInsurance { get; set; }
        public decimal Confirmation { get; set; }
        public decimal DiscountedRate { get; set; }
        public decimal DeliveryConfirmation { get; set; }
        public decimal DimensionalWeight { get; set; }
        public decimal? FirstMileSurcharge { get; set; }
        public decimal FuelSurcharge { get; set; }
        public decimal Handling { get; set; }
        public decimal Insurance { get; set; }
        public decimal PeakSurcharge { get; set; }
        public decimal Pickup { get; set; }
        public decimal RatedAsWeight { get; set; }
        public decimal Total { get; set; }
        public string Currency { get; set; }
    }
}
