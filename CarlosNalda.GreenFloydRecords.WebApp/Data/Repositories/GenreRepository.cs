using CarlosNalda.GreenFloydRecords.WebApp.Data.Persistence;

namespace CarlosNalda.GreenFloydRecords.WebApp.Data.Repositories
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
