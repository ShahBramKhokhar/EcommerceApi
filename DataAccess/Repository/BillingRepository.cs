using WebRexErpAPI.Data.Repository.GenericRepository;
using WebRexErpAPI.Models;

namespace WebRexErpAPI.Data.Repository
{

    public interface IBillingRepository : IGenericRepository<Billing>
    {

    }

    public class BillingRepository : GenericRepository<Billing>, IBillingRepository
    {
        public BillingRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
