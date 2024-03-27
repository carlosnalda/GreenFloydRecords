using CarlosNalda.GreenFloydRecords.Persistence.DatabaseInitializer;
using CarlosNalda.GreenFloydRecords.Persistence;
using CarlosNalda.GreenFloydRecords.Infrastructure;
using CarlosNalda.GreenFloydRecords.Application.Contracts.Infrastructure;
using CarlosNalda.GreenFloydRecords.Application;
using CarlosNalda.GreenFloydRecords.WebApp.Middleware;

namespace CarlosNalda.GreenFloydRecords.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddInfrastructureServices(builder.Configuration);

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                SeedImageFiles(app);
                SeedDatabase(app);

                // app.UseCustomExceptionHandler();
            }
            else
            {
                // app.UseExceptionHandler("/InvalidAction/Error");

                // Hacer version para developer, que mande a una pagina not found
                app.UseCustomExceptionHandler();

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseStatusCodePagesWithReExecute("/InvalidAction/PageNotFound/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // app.UseCustomExceptionHandler();

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
                var defaultImageFileInitializer = scope.ServiceProvider.GetRequiredService<IImageFileManager>();
                defaultImageFileInitializer.Seed();
            }
        }
    }
}