using System.Linq.Expressions;
using WebRexErpAPI.DataAccess.Models;

namespace WebRexErpAPI.Data.Repository.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> Add(T entity);
        Task<bool> AddRange(IEnumerable<T> entity);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllAsync(bool isDistinct = false);
        List<T> GetAll(bool isDistinct = false);
        List<T> GetAllData(bool isDistinct = false);
        T GetById(Guid id);
        Task<T> GetByIdAsync(object id);
        Task<bool> Remove(int id);
        Task<int> InsertItemAsync(T entity);
        Task<int> UpdateItemAsync(T entity);
        Task<bool> RemoveRange(IEnumerable<T> entities);
        List<T> GetMany(Expression<Func<T, bool>> where);
        Task<bool> Update(T entity);
        Task<bool> UpdateRange(IEnumerable<T> entity);
        Task<int> CountAsync(CancellationToken cancellationToken);
        int Count(Expression<Func<T, bool>> where);
        Task<int> CountAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> where);
        Task<int> CountAsync(Expression<Func<T, bool>> where);
        bool Exists(object primaryKey);
        T GetFirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<T> GetFirstOrDefaultAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> predicate);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        bool Any(Expression<Func<T, bool>> expression);
   



        // Task<bool> AnyAsync(Expression<Func<T, bool>> where);
    }
        
}
