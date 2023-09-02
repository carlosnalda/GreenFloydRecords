using CarlosNalda.GreenFloydRecords.WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CarlosNalda.GreenFloydRecords.WebApp.DatabaseInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;

        public DbInitializer(ApplicationDbContext db)
        {
            _db = db;
        }


        public void Initialize()
        {
            //migrations if they are not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }

            if (!_db.Set<Genre>().Any())
            {
                _db.Genre.Add(
                    new Genre
                    {
                        Id = Guid.NewGuid(),
                        Name = "Genre 1",
                    });

                _db.SaveChanges();
            }
        }
    }
}
