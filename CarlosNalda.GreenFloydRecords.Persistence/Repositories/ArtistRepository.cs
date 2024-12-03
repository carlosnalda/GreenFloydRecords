using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CarlosNalda.GreenFloydRecords.Persistence.Repositories
{
    public class ArtistRepository : BaseRepository<Artist>, IArtistRepository
    {
        public ArtistRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Artist?> GetArtistWithChildEntitiesAsync(Guid id, string? includeProperties = null)
        {
            var query = _applicationDbContext.Set<Artist>()
               .AsQueryable();

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            var foo = await query
                .FirstOrDefaultAsync(artist => artist.Id == id);
            return foo;
        }
    }
}
