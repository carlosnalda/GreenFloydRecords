using CarlosNalda.GreenFloydRecords.WebApp.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarlosNalda.GreenFloydRecords.WebApp.Data.Repositories
{
    public class VinylRecordRepository : BaseRepository<VinylRecord>, IVinylRecordRepository
    {
        public VinylRecordRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
