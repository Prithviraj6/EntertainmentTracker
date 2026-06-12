using EntertainmentTracker.Application.Abstractions.Persistence;
using EntertainmentTracker.Domain.Animes;
using Microsoft.EntityFrameworkCore;

namespace EntertainmentTracker.Infrastructure.Persistence.Repositories
{
    public sealed class GenreRepository
    : IGenreRepository
    {
        private readonly AppDbContext _dbContext;

        public GenreRepository(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Genre?> GetByMalIdAsync(
            int malId,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Genres
                .FirstOrDefaultAsync(
                    x => x.MalId == malId,
                    cancellationToken);
        }

        public async Task AddAsync(
            Genre genre,
            CancellationToken cancellationToken = default)
        {
            await _dbContext.Genres.AddAsync(
                genre,
                cancellationToken);
        }

        public async Task<IReadOnlyList<Genre>> GetByMalIdsAsync(
            IEnumerable<int> malIds,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Genres
                .Where(x => malIds.Contains(x.MalId))
                .ToListAsync(cancellationToken);
        }
    }
}
