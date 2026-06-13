using EntertainmentTracker.Application.Abstractions.Persistence;
using EntertainmentTracker.Domain.Animes;
using EntertainmentTracker.Domain.Enums;
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
            UserAnimeStatus? status,
            CancellationToken cancellationToken = default)
        {
            var query =
                _dbContext.UserAnime
                    .Include(x => x.Anime)
                    .Where(x => x.UserId == userId);

            if (status.HasValue)
            {
                query = query.Where(
                    x => x.Status == status.Value);
            }

            return await query.ToListAsync(
                cancellationToken);
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
