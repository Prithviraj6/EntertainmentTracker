using EntertainmentTracker.Application.Abstractions.Persistence;
using EntertainmentTracker.Domain.Animes;
using Microsoft.EntityFrameworkCore;

namespace EntertainmentTracker.Infrastructure.Persistence.Repositories
{
    public sealed class AnimeRepository
    : IAnimeRepository
    {
        private readonly AppDbContext _dbContext;

        public AnimeRepository(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Anime?> GetByMalIdAsync(
            int malId,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Animes
                .Include(x => x.AnimeGenres)
                    .ThenInclude(x => x.Genre)
                .FirstOrDefaultAsync(
                    x => x.MalId == malId,
                    cancellationToken);
        }

        public async Task AddAsync(
            Anime anime,
            CancellationToken cancellationToken = default)
        {
            await _dbContext.Animes.AddAsync(
                anime,
                cancellationToken);
        }

        public async Task<Anime?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Animes
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }
    }
}
