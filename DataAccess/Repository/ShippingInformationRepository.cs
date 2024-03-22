using WebRexErpAPI.Data.Repository.GenericRepository;
using WebRexErpAPI.Models;

namespace WebRexErpAPI.Data.Repository
{

    public interface IShoppingInformationRepository : IGenericRepository<ShoppingInformation>
    {

    }

    public class ShoppingInformationRepository : GenericRepository<ShoppingInformation>, IShoppingInformationRepository
    {
        public ShoppingInformationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
