using WebRexErpAPI.Data.Repository.GenericRepository;
using WebRexErpAPI.Models;
using System.Linq.Expressions;

namespace WebRexErpAPI.Data.Repository
{

    public interface ICategoryRepository : IGenericRepository<Category>
    {

    }

    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<List<Category>> GetAllAsync(bool isDistinct = false)
        {
            //return base.GetAll().DistinctBy(d => d.Name).OrderBy(o => o.Name).ToList();
            var data = await base.GetAllAsync();
            data = data.DistinctBy(d => d.Name).OrderBy(o => o.Name).ToList();
            return data;

        }

        public override List<Category> GetAll(bool isDistinct = false)
        {
            return base.GetAll().DistinctBy(d => d.Name).OrderBy(o => o.Name).ToList();
        }

        public override List<Category> GetMany(Expression<Func<Category, bool>> where)
        {
            return base.GetMany(where).DistinctBy(d => d.Name).OrderBy(o => o.Name).ToList();
        }
    }
}
