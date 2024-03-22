using WebRexErpAPI.Business.Billing.Dto;
using System.Threading.Tasks;

namespace WebRexErpAPI.Business.Billing
{
    public interface IBillingService
    {
        Task<WebRexErpAPI.Models.Billing> SaveBillingAsync(BillingDto input);
        Task UpdateBillingAsync(BillingDto input);
        Task<bool> DeletBillingAsync(int id);
        Task<List<BillingDto>> getUserBilling(int userId);

    }
}
