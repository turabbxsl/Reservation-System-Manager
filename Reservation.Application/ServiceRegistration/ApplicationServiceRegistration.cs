using Microsoft.Extensions.DependencyInjection;
using Reservation.Application.Interfaces.Services;
using Reservation.Application.Services;
using System.Reflection;

namespace Reservation.Application.ServiceRegistration
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordService, PasswordService>();

            return services;
        }

    }
}
