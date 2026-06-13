using EntertainmentTracker.Domain.Animes;

namespace EntertainmentTracker.Application.Abstractions.Persistence
{
    public interface IAnimeRepository
    {
        Task<Anime?> GetByMalIdAsync(
            int malId,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            Anime anime,
            CancellationToken cancellationToken = default);

        Task<Anime?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);
    }
}
