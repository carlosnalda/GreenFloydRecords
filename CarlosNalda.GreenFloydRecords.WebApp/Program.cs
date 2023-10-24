using CarlosNalda.GreenFloydRecords.WebApp.Data;
using CarlosNalda.GreenFloydRecords.WebApp.Data.Persistence;
using CarlosNalda.GreenFloydRecords.WebApp.Data.Repositories;
using CarlosNalda.GreenFloydRecords.WebApp.DatabaseInitializer;
using CarlosNalda.GreenFloydRecords.WebApp.ImageFileInitializer;
using CarlosNalda.GreenFloydRecords.WebApp.Infrastructure.ImageManager;
using Microsoft.EntityFrameworkCore;

namespace CarlosNalda.GreenFloydRecords.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder
               .Configuration
               .GetConnectionString("ConnectionString")
                   ?? throw new InvalidOperationException("Connection string 'ConnectionString' not found.");
            builder
                .Services
                .AddDbContext<ApplicationDbContext>(options => options
                    .UseSqlServer(connectionString));
            builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<IGenreRepository, GenreRepository>();
            builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
            builder.Services.AddScoped<IVinylRecordRepository, VinylRecordRepository>();
            builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            builder.Services.AddScoped<IDefaultImageFileInitializer, DefaultImageFileInitializer>();
            builder.Services.AddScoped<IImageFileManager, ImageFileManager>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                SeedImageFiles(app);
                SeedDatabase(app);
            }
            else
            {
                app.UseExceptionHandler("/InvalidAction/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/InvalidAction/PageNotFound/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        private static void SeedDatabase(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                dbInitializer.Initialize();
            }
        }

        private static void SeedImageFiles(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var defaultImageFileInitializer = scope.ServiceProvider.GetRequiredService<IDefaultImageFileInitializer>();
                defaultImageFileInitializer.Initialize();
            }
        }
    }
}