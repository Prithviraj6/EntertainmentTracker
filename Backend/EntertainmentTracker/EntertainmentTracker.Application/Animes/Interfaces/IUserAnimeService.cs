using EntertainmentTracker.Application.Animes.DTOs;

namespace EntertainmentTracker.Application.Animes.Interfaces
{
    public interface IUserAnimeService
    {
        Task<UserAnimeResponse> AddAsync(
            Guid userId,
            AddUserAnimeRequest request,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<UserAnimeResponse>> GetByUserAsync(
            Guid userId,
            CancellationToken cancellationToken = default);
    }
}
