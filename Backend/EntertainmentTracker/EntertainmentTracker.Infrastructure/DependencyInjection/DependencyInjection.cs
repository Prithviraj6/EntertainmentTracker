using EntertainmentTracker.Application.Abstractions.Persistence;
using EntertainmentTracker.Application.Animes.Interfaces;
using EntertainmentTracker.Application.Animes.Services;
using EntertainmentTracker.Application.Auth.Interfaces;
using EntertainmentTracker.Application.Auth.Services;
using EntertainmentTracker.Application.Common.Interfaces;
using EntertainmentTracker.Infrastructure.Authentication;
using EntertainmentTracker.Infrastructure.Common;
using EntertainmentTracker.Infrastructure.External.Jikan;
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

            //Anime
            services.AddHttpClient<IJikanClient, JikanClient>(client =>
            {
                client.BaseAddress =
                    new Uri("https://api.jikan.moe/v4/");
            });

            services.AddScoped<IAnimeService, AnimeService>();
            services.AddScoped<IAnimeRepository, AnimeRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IUserAnimeRepository, UserAnimeRepository>();

            return services;
        }
    }
}
