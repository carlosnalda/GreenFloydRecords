using CarlosNalda.GreenFloydRecords.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarlosNalda.GreenFloydRecords.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Genre> Genre { get; set; }

        public DbSet<Artist> Artist { get; set; }

        public DbSet<VinylRecord> VinylRecord { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
            : base(dbContextOptions)

        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<VinylRecord>()
                .Property(p => p.Rate)
                .HasColumnType("decimal(4,2)");

            builder.Entity<VinylRecord>()
                .Property(p => p.Price)
                .HasColumnType("decimal(14,2)");

            base.OnModelCreating(builder);
        }
    }
}
