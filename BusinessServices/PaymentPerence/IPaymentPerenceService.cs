using WebRexErpAPI.Business.PaymentPreference.Dto;

namespace WebRexErpAPI.Business.PaymentPreference
{
    public interface IPaymentPreferenceService
    {
        Task<WebRexErpAPI.Models.PaymentPreference> SavePaymentPreferenceAsync(PaymentPreferenceDto input);
        Task UpdatePaymentPreferenceAsync(PaymentPreferenceDto input);
        Task<bool> DeletPaymentPreferenceAsync(int id);
        Task<List<PaymentPreferenceDto>> getUserPaymentPreference(int userId);

    }
}
