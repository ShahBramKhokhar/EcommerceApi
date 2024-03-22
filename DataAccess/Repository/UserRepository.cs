using Microsoft.EntityFrameworkCore;
using WebRexErpAPI.Data.Repository.GenericRepository;
using WebRexErpAPI.DataAccess.Models;
using WebRexErpAPI.Models;

namespace WebRexErpAPI.Data.Repository
{

    public interface IUserRepository : IGenericRepository<User>
    {

    }

    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
   

}
