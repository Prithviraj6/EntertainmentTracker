using EntertainmentTracker.Application.Animes.DTOs;
using EntertainmentTracker.Domain.Enums;

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
            UserAnimeStatus? status,
            CancellationToken cancellationToken = default);

        Task UpdateProgressAsync(
            Guid userId,
            Guid animeId,
            UpdateProgressRequest request,
            CancellationToken cancellationToken = default);

        Task UpdateStatusAsync(
            Guid userId,
            Guid animeId,
            UpdateStatusRequest request,
            CancellationToken cancellationToken = default);
    }
}
