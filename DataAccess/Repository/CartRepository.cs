using WebRexErpAPI.Data.Repository.GenericRepository;
using WebRexErpAPI.Models;

namespace WebRexErpAPI.Data.Repository
{

    public interface ICartRepository : IGenericRepository<Cart>
    {

    }

    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
