using WebRexErpAPI.Data.Repository.GenericRepository;
using WebRexErpAPI.DataAccess.Models;

namespace WebRexErpAPI.Data.Repository
{

    public interface IAllowItemOfferRepository : IGenericRepository<AllowItemOffer>
    {

    }                                                                              

    public class AllowItemOfferRepository : GenericRepository<AllowItemOffer>, IAllowItemOfferRepository
    {
        public AllowItemOfferRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
