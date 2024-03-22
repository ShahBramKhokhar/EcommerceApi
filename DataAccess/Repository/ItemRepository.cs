
using WebRexErpAPI.Data.Repository.GenericRepository;
using WebRexErpAPI.Models;
using System.Linq.Expressions;

namespace WebRexErpAPI.Data.Repository
{

    public interface IItemRepository : IGenericRepository<Item>
    {

    }

    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        Func<Item, bool> Wherepredicate = i => i.Quantity > 0 && i.IsDeactivate == false;

        public ItemRepository(ApplicationDbContext context) : base(context)
        {

        }


        public override async Task<List<Item>> GetAllAsync(bool isDistinct = false)
        {
            //var items = base.GetAll().Where(Wherepredicate);
            //items = items.OrderByDescending(item => item.Id); 
            //return  items.ToList(); 
            var items = await base.GetAllAsync(); // Assuming GetAllAsync exists
            if (Wherepredicate != null)
            {
                items = items.Where(Wherepredicate).DistinctBy(a =>a.QbRecordId).ToList();
            }

            items = items.OrderByDescending(item => item.Id).ToList();
            return items;



        }

        public override List<Item> GetAll(bool isDistinct = false)
        {
            var items = base.GetAll().Where(Wherepredicate).DistinctBy(a => a.QbRecordId);

            items = items.OrderByDescending(item => item.Id); 
            return items.ToList();
        }
        public async Task<int> InsertItemAsync(Item entity)
        {
            return await base.InsertItemAsync(entity);
        }

        public async Task<int> UpdateItemAsync(Item entity)
        {
            return await base.UpdateItemAsync(entity);
        }


        public override List<Item> GetMany(Expression<Func<Item, bool>> where)
        {
            var items = base.GetMany(where).Where(Wherepredicate).DistinctBy(a => a.QbRecordId);
            items = items.OrderByDescending(item => item.Id);
            return items.ToList();
        }

        public new IEnumerable<Item> FindAll(Expression<Func<Item, bool>> expression)
        {
            var items = base.FindAll(expression).Where(Wherepredicate).DistinctBy(a => a.QbRecordId);
            items = items.OrderByDescending(item => item.Id);
            return items.ToList();
        }
    }
}
