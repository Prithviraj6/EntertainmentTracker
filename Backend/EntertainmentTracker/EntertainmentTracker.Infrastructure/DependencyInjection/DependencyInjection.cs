using EntertainmentTracker.Application.Abstractions.Persistence;
using EntertainmentTracker.Application.Auth.Interfaces;
using EntertainmentTracker.Application.Auth.Services;
using EntertainmentTracker.Application.Common.Interfaces;
using EntertainmentTracker.Infrastructure.Authentication;
using EntertainmentTracker.Infrastructure.Common;
using EntertainmentTracker.Infrastructure.Persistence;
using EntertainmentTracker.Infrastructure.Persistence.Repositories;
using EntertainmentTracker.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EntertainmentTracker.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("Database"));
            });

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.Configure<JwtOptions>(configuration.GetSection(
                JwtOptions.SectionName));

            services.AddScoped<IJwtProvider, JwtProvider>();

            services.AddScoped<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
