using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Persistence.DatabaseInitializer;
using CarlosNalda.GreenFloydRecords.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarlosNalda.GreenFloydRecords.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            var connectionString = configuration
                .GetConnectionString("ConnectionString")
                    ?? throw new InvalidOperationException("Connection string 'ConnectionString' not found.");

            services
                .AddDbContext<ApplicationDbContext>(options => options
                    .UseSqlServer(connectionString));

            
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<IVinylRecordRepository, VinylRecordRepository>();
            services.AddScoped<IDbInitializer, DbInitializer>();

            return services;
        }
    }
}
