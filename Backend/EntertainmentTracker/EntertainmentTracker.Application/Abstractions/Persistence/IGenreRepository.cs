using EntertainmentTracker.Domain.Animes;

namespace EntertainmentTracker.Application.Abstractions.Persistence
{
    public interface IGenreRepository
    {
        Task<Genre?> GetByMalIdAsync(
            int malId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Genre>> GetByMalIdsAsync(
            IEnumerable<int> malIds,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            Genre genre,
            CancellationToken cancellationToken = default);
    }
}
