using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebRexErpAPI.DataAccess.Models;
using WebRexErpAPI.Models;

namespace WebRexErpAPI.Data.Repository.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        private readonly ApplicationDbContext _context;
        protected DbSet<T> dbSet;
     

        public GenericRepository(
           ApplicationDbContext context  )
        {
            _context = context;
            dbSet = _context.Set<T>();
        }
        public async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

    

        public async Task<bool> AddRange(IEnumerable<T> entity)
        {
            await dbSet.AddRangeAsync(entity);
            return true;
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> expression)
        {
            return dbSet.Where(expression);
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> expression)
        {
            return await dbSet.Where(expression).ToListAsync();
        }


        public virtual Task<List<T>> GetAllAsync(bool isDistinct = false)
        {
            if (isDistinct)
                return dbSet.Distinct().ToListAsync();
            else
                return dbSet.ToListAsync();
        }

        public virtual List<T> GetAll(bool isDistinct = false)
        {
            if (isDistinct)
                return dbSet.Distinct().ToList();
            else
                return dbSet.ToList();
        }


        public virtual List<T> GetAllData(bool isDistinct = false)
        {
            if (isDistinct)
                return dbSet.Distinct().ToList();
            else
                return dbSet.ToList();
        }

        public T GetById(Guid id)
        {
            return dbSet.Find(id);
        }

        /// <summary>
        /// Generic get method on the basis of id for entities asynchronously
        /// </summary>
        /// <param name="id">Should be primary key of table</param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<bool> Remove(int id)
        {
            var t = await dbSet.FindAsync(id);

            if (t != null)
            {
                dbSet.Remove(t);
                return true;
            }
            else
                return false;
        }


        public Task<bool> RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            return Task.FromResult(true);
        }


        /// <summary>
        /// generic method to get many record on the basis of a condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual List<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }

        public Task<bool> Update(T entity)
        {
             dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(true);
        }

        public Task<bool> UpdateRange(IEnumerable<T> entity)
        {
              dbSet.AttachRange(entity);
              _context.Entry(entity).State = EntityState.Modified;
              return Task.FromResult(true);
        }

        public Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return dbSet.CountAsync(cancellationToken);


        }
      
     

        /// <summary>
        /// generic count method , fetches count for the entities on the basis of condition
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int Count(Expression<Func<T, bool>> where)
        {
            return dbSet.Count(where);
        }

        /// <summary>
        /// generic count method , fetches count for the entities on the basis of condition asynchronously
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public Task<int> CountAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> where)
        {
            return dbSet.CountAsync(where, cancellationToken);


        }

        /// <summary>
        /// generic get method , fetches count for the entities on the basis of condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public Task<int> CountAsync(Expression<Func<T, bool>> where)
        {
            return dbSet.CountAsync(where);
        }

        /// <summary>
        /// Generic method to check if entity exists
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public bool Exists(object primaryKey)
        {
            return GetById((Guid)primaryKey) != null;
        }


        /// <summary>
        /// The first record matching the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record containing the first record matching the specified criteria</returns>
        public T GetFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return dbSet.FirstOrDefault<T>(predicate);
        }

        /// <summary>
        /// The first record matching the specified criteria asynchronously
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record containing the first record matching the specified criteria</returns>
        public Task<T> GetFirstOrDefaultAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> predicate)
        {
            return dbSet.FirstOrDefaultAsync<T>(predicate, cancellationToken);
        }


        /// <summary>
        /// The first record matching the specified criteria asynchronously
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record containing the first record matching the specified criteria</returns>
        public Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return dbSet.FirstOrDefaultAsync<T>(predicate);
        }
        public async Task<int> InsertItemAsync(T entity)
        {
            try
            {
                await dbSet.AddAsync(entity);
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Handle exception as needed
                return -1; // Return an appropriate value indicating failure
            }
        }

        public async Task<int> UpdateItemAsync(T entity)
        {
            try
            {
                dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Handle exception as needed
                return -1; // Return an appropriate value indicating failure
            }
        }
    
     

        



        public virtual bool Any(Expression<Func<T, bool>> expression)
        {
            var isExit = dbSet.Any(expression);
            if (isExit)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region IDisposable Implementation

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        


        #endregion
    }
}
