using Reservation.Persistence.ServiceRegistration;
using Reservation.Application.ServiceRegistration;
using Reservation.API.Middleware;

namespace Reservation.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;

            builder.Services.AddControllers();

            builder.Services.AddPersistenceServices(configuration);
            builder.Services.AddApplicationServices();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<CustomExceptionMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
