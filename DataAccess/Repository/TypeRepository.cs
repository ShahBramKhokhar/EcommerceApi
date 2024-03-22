using WebRexErpAPI.Data.Repository.GenericRepository;
using WebRexErpAPI.Models;
using System.Linq.Expressions;
using Type = WebRexErpAPI.Models.Type;

namespace WebRexErpAPI.Data.Repository
{

    public interface ITypeRepository : IGenericRepository<Type>
    {

    }

    public class TypeRepository : GenericRepository<Type>, ITypeRepository
    {
        public TypeRepository(ApplicationDbContext context) : base(context)
        {
        }


        public override async Task<List<Type>> GetAllAsync(bool isDistinct = false)
        {
            // return base.GetAll().DistinctBy(d => d.Name).OrderBy(o => o.Name).ToList();

            var data = await base.GetAllAsync();
            data = data.DistinctBy(d => d.Name).OrderBy(o => o.Name).ToList();
            return data;
        }

        public override List<Type> GetAll(bool isDistinct = false)
        {
            return base.GetAll().DistinctBy(d => d.Name).OrderBy(o => o.Name).ToList();
        }

        public override List<Type> GetMany(Expression<Func<Type, bool>> where)
        {
            return base.GetMany(where).DistinctBy(d => d.Name).OrderBy(o => o.Name).ToList();
        }
    }
}
