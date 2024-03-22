using WebRexErpAPI.Data.Repository.GenericRepository;
using WebRexErpAPI.Models;

namespace WebRexErpAPI.Data.Repository
{

    public interface IOrderRepository : IGenericRepository<Order>
    {

    }

    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
