using WebRexErpAPI.Data.Repository.GenericRepository;
using WebRexErpAPI.DataAccess.Models;

namespace WebRexErpAPI.Data.Repository
{

    public interface ISaveLaterRepository : IGenericRepository<SaveLater>
    {
    }

    public class SaveLaterRepository : GenericRepository<SaveLater>, ISaveLaterRepository
    {
        public SaveLaterRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
