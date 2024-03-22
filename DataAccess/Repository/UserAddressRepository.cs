using WebRexErpAPI.Data.Repository.GenericRepository;
namespace WebRexErpAPI.Data.Repository
{

    public interface IUserAddressRepository : IGenericRepository<WebRexErpAPI.Models.UserAddress>
    {

    }

    public class UserAddressRepository : GenericRepository<WebRexErpAPI.Models.UserAddress>, IUserAddressRepository
    {
        public UserAddressRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
