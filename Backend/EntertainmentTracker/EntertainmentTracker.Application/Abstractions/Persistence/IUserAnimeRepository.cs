using EntertainmentTracker.Domain.Animes;
using EntertainmentTracker.Domain.Enums;

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
            UserAnimeStatus? status,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            UserAnime userAnime,
            CancellationToken cancellationToken = default);

        Task RemoveAsync(
            UserAnime userAnime);
    }
}
