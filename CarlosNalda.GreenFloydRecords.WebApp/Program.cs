using CarlosNalda.GreenFloydRecords.Persistence.DatabaseInitializer;
using CarlosNalda.GreenFloydRecords.Persistence;
using CarlosNalda.GreenFloydRecords.Infrastructure;
using CarlosNalda.GreenFloydRecords.Application.Contracts.Infrastructure;

namespace CarlosNalda.GreenFloydRecords.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddInfrastructureServices(builder.Configuration);

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
                var defaultImageFileInitializer = scope.ServiceProvider.GetRequiredService<IImageFileManager>();
                defaultImageFileInitializer.Seed();
            }
        }
    }
}