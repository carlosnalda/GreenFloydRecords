using CarlosNalda.GreenFloydRecords.Application.Contracts.Infrastructure;
using CarlosNalda.GreenFloydRecords.Infrastructure.ImageManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarlosNalda.GreenFloydRecords.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            services.AddScoped<IImageFileManager, ImageFileManager>();

            return services;
        }
    }
}
