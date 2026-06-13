using EntertainmentTracker.Application.Abstractions.Persistence;
using EntertainmentTracker.Domain.Animes;
using Microsoft.EntityFrameworkCore;

namespace EntertainmentTracker.Infrastructure.Persistence.Repositories
{
    public sealed class UserAnimeRepository
    : IUserAnimeRepository
    {
        private readonly AppDbContext _dbContext;

        public UserAnimeRepository(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserAnime?> GetAsync(
            Guid userId,
            Guid animeId,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.UserAnime
                .FirstOrDefaultAsync(
                    x =>
                        x.UserId == userId &&
                        x.AnimeId == animeId,
                    cancellationToken);
        }

        public async Task<IReadOnlyList<UserAnime>> GetByUserAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.UserAnime
                .Include(x => x.Anime)
                .Where(x => x.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(
            UserAnime userAnime,
            CancellationToken cancellationToken = default)
        {
            await _dbContext.UserAnime.AddAsync(
                userAnime,
                cancellationToken);
        }
    }
}
