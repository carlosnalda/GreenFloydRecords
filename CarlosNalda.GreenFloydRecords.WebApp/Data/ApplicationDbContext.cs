using Microsoft.EntityFrameworkCore;

namespace CarlosNalda.GreenFloydRecords.WebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Genre> Genre { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
            : base(dbContextOptions)

        {
        }
    }
}
