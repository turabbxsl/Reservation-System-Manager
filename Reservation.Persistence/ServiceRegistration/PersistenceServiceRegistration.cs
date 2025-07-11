using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reservation.Application.Interfaces;
using Reservation.Application.Interfaces.Services;
using Reservation.Application.Services;
using Reservation.Domain.Entities;
using Reservation.Persistence.Configuration.Identity;
using Reservation.Persistence.Context;
using Reservation.Persistence.Repositories;
namespace Reservation.Persistence.ServiceRegistration
{
    public static class PersistenceServiceRegistration
    {

        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ReservationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentityCore<User>(options => {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 1;

                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ReservationDbContext>()
            .AddErrorDescriber<CustomIdentityErrorDescriber>()
            .AddSignInManager<SignInManager<User>>()
            .AddDefaultTokenProviders(); ;

            services.AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddCookie(IdentityConstants.ApplicationScheme, options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;
            });

            services.AddScoped<IUnitofWork, UnitofWork>();
            services.AddScoped<ICompanyService, CompanyService>();

            return services;
        }


    }
}
