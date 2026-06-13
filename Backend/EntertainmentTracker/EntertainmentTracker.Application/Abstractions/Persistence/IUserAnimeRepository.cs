using EntertainmentTracker.Domain.Animes;

namespace EntertainmentTracker.Application.Abstractions.Persistence
{
    public interface IUserAnimeRepository
    {
        Task<UserAnime?> GetAsync(
            Guid userId,
            Guid animeId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<UserAnime>> GetByUserAsync(
            Guid userId,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            UserAnime userAnime,
            CancellationToken cancellationToken = default);
    }
}
