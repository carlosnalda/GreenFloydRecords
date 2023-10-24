using CarlosNalda.GreenFloydRecords.WebApp.Data.Persistence;

namespace CarlosNalda.GreenFloydRecords.WebApp.Data.Repositories
{
    public class ArtistRepository : BaseRepository<Artist>, IArtistRepository
    {
        public ArtistRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
