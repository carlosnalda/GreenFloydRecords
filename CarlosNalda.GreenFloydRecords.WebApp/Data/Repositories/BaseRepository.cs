using CarlosNalda.GreenFloydRecords.WebApp.Data;
using CarlosNalda.GreenFloydRecords.WebApp.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarlosNalda.GreenFloydRecords.WebApp.Data.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _applicationDbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _applicationDbContext = dbContext;
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            T? t = await _applicationDbContext.Set<T>().FindAsync(id);
            return t;
        }

        public async Task<T?> GetByIdAsNoTrackingAsync(Guid id)
        {
            T? t = await _applicationDbContext.Set<T>().FindAsync(id);
            if (t != null)
            {
                _applicationDbContext.Entry(t).State = EntityState.Detached;
            }

            return t;
        }

        public async Task<IReadOnlyList<T>> ListAllAsync(string? includeProperties = null)
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.ToListAsync();
        }

        public async virtual Task<IReadOnlyList<T>> GetPagedResponseAsync(int page, int size)
        {
            return await _applicationDbContext
                .Set<T>()
                .Skip((page - 1) * size)
                .Take(size)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _applicationDbContext
                .Set<T>()
                .AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _applicationDbContext.Update(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _applicationDbContext.Set<T>().Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
