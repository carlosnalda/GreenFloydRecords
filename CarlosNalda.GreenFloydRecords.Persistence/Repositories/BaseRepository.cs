using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarlosNalda.GreenFloydRecords.Persistence.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _applicationDbContext;
        protected readonly DbSet<T> _entities;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _applicationDbContext = dbContext;
            _entities = _applicationDbContext.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            T? t = await _entities.FindAsync(id);
            return t;
        }

        public async Task<T?> GetByIdAsNoTrackingAsync(Guid id)
        {
            T? t = await _entities.FindAsync(id);
            if (t != null)
            {
                _entities.Entry(t).State = EntityState.Detached;
            }

            return t;
        }

        public async Task<IReadOnlyList<T>> ListAllAsync(string? includeProperties = null)
        {
            IQueryable<T> query = _entities;
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
            return await _entities
                .Skip((page - 1) * size)
                .Take(size)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _entities.Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
