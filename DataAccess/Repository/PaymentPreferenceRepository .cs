using WebRexErpAPI.Data.Repository.GenericRepository;
using WebRexErpAPI.Models;

namespace WebRexErpAPI.Data.Repository
{

    public interface IPaymentPreferenceRepository : IGenericRepository<PaymentPreference>
    {

    }

    public class PaymentPreferenceRepository : GenericRepository<PaymentPreference>, IPaymentPreferenceRepository
    {
        public PaymentPreferenceRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
