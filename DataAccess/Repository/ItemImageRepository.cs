using WebRexErpAPI.Data.Repository.GenericRepository;
using WebRexErpAPI.Models;

namespace WebRexErpAPI.Data.Repository
{

    public interface IItemImageGalleryRepository : IGenericRepository<ItemImageGallery>
    {

    }

    public class ItemImageGalleryRepository : GenericRepository<ItemImageGallery>, IItemImageGalleryRepository
    {
        public ItemImageGalleryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
