using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Reservation.Application.Behaviours;
using Reservation.Application.Features.Users.Validators;
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

            services.AddValidatorsFromAssembly(typeof(CreateStaffMemberCommandValidator).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddAutoMapper(_ => { }, Assembly.GetExecutingAssembly());

            return services;
        }

    }
}
