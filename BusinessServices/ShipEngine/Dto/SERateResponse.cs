namespace WebRexErpAPI.BusinessServices.ShipEngine.Dto
{
    public partial class SERateResponse
    {
        public SERate[]? rates { get; set; }

        public object[]? invalid_rates { get; set; }

        public string? rate_request_id { get; set; }

        public string? shipment_id { get; set; }

        public DateTimeOffset? created_at { get; set; }

        public string? status { get; set; }

        public object[]? errors { get; set; }
    }
}
