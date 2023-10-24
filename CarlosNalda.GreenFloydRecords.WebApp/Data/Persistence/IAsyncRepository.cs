using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarlosNalda.GreenFloydRecords.WebApp.Data.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid Id);
        Task<T?> GetByIdAsNoTrackingAsync(Guid id);
        Task<IReadOnlyList<T>> ListAllAsync(string? includeProperties = null);
        Task<IReadOnlyList<T>> GetPagedResponseAsync(int page, int size);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        
    }
}
