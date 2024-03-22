using WebRexErpAPI.Business.Billing.Dto;
using WebRexErpAPI.Business.Industry.Dto;
using WebRexErpAPI.Business.PaymentPreference.Dto;
using WebRexErpAPI.Business.ShippingInformation.Dto;
using WebRexErpAPI.Services.Account.Dto;

namespace WebRexErpAPI.BusinessServices.CheckOut.Dto
{
    public class CheckoutInputModel
    {
        public string? OrderNo { get; set; }
        public List<ItemDto>? Items { get; set; }
        public decimal CartTotalAmount { get; set; } = 0;
        public decimal CartTotalShippingAmount { get; set; } = 0;
        public decimal CartPackagingAmount { get; set; } = 0;
        public decimal TotalAmount { get; set; }  = 0 ;
        public decimal TotalTax { get; set; } = 0;
        public ShippingInformationDto? SelectedShippingOption { get; set; }
        public BillingDto? SelectedBillingOption { get; set; }
        public int? SelectedPPOption { get; set; }
        public PaymentPreferenceDto? PPDetail { get; set; }
        public UserVM? UserDetail { get; set; }
        public long? StripePayment { get; set; }
    }

   
}
