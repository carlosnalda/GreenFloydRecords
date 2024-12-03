using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarlosNalda.GreenFloydRecords.Persistence.Repositories
{
    public class VinylRecordRepository : BaseRepository<VinylRecord>, IVinylRecordRepository
    {
        public VinylRecordRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ICollection<VinylRecord>> ListForArtistAsync(Guid artistId) =>
             await _applicationDbContext.Set<VinylRecord>()
                .Where(vinylReacord => vinylReacord.ArtistId == artistId)
                .ToListAsync();

        public async Task<VinylRecord?> SingleOrDefaultForArtistAsync(Guid artistId, Guid vinylRecordId) =>
            await _applicationDbContext.Set<VinylRecord>()
                .SingleOrDefaultAsync(vinylReacord => vinylReacord.ArtistId == artistId &&
                    vinylReacord.Id == vinylRecordId);
    }
}
