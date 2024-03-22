using WebRexErpAPI.Data.Repository.GenericRepository;
using WebRexErpAPI.Models;
using System.Linq.Expressions;

namespace WebRexErpAPI.Data.Repository
{

    public interface IIndustryRepository : IGenericRepository<Industry>
    {

    }

    public class IndustryRepository : GenericRepository<Industry>, IIndustryRepository
    {
        public IndustryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<List<Industry>> GetAllAsync(bool isDistinct = false)
        {
            // return base.GetAll().DistinctBy(d => d.Name).OrderBy(o => o.Name).ToList(); ;

            var data = await base.GetAllAsync();
            data = data.DistinctBy(d => d.Name).OrderBy(o => o.Name).ToList();
            return data;
        }

        public override List<Industry> GetAll(bool isDistinct = false)
        {
            return base.GetAll().DistinctBy(d => d.Name).OrderBy(o => o.Name).ToList();
        }

        public override List<Industry> GetMany(Expression<Func<Industry, bool>> where)
        {
            return base.GetMany(where).DistinctBy(d => d.Name).OrderBy(o => o.Name).ToList();
        }
    }
}
