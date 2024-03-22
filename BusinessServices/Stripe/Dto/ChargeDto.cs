namespace WebRexErpAPI.BusinessServices.Stripe.Dto
{
    public class ChargeResponsDto
    {
        public string? Status { get; set; }
        public string? BalanceTransactionId { get; set; }
    }

    public class ChargeRequestDto
    {
        public string? StripeEmail { get; set; }
        public string? StripToken { get; set; }
        public long?  Amount { get; set; }
        public string? Name { get; internal set; }
    }

    public class PaymentIntentDto
    {
        public long? Amount { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }
        public string LatestChargeId { get; set; }

    }
}
