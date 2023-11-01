using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Domain.Entities;

namespace CarlosNalda.GreenFloydRecords.Persistence.Repositories
{
    public class VinylRecordRepository : BaseRepository<VinylRecord>, IVinylRecordRepository
    {
        public VinylRecordRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
