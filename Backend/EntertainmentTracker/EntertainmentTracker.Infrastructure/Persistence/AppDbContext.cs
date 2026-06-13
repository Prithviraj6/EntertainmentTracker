using EntertainmentTracker.Domain.Animes;
using EntertainmentTracker.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace EntertainmentTracker.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //User
        public DbSet<User> Users => Set<User>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        //Anime
        public DbSet<Anime> Animes => Set<Anime>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<AnimeGenre> AnimeGenres => Set<AnimeGenre>();
        public DbSet<UserAnime> UserAnime => Set<UserAnime>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
