using CarlosNalda.GreenFloydRecords.Persistence;
using CarlosNalda.GreenFloydRecords.Infrastructure;
using CarlosNalda.GreenFloydRecords.Application;
using CarlosNalda.GreenFloydRecords.Api.Middleware;

namespace CarlosNalda.GreenFloydRecords.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddInfrastructureServices(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCustomExceptionHandlerForDev();
            }
            else
            {
                app.UseCustomExceptionHandlerForProd();
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}