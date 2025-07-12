﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Reservation.Application.ServiceRegistration
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }

    }
}
