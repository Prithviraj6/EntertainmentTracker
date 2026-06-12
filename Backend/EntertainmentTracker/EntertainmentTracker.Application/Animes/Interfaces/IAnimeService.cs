using EntertainmentTracker.Application.Animes.DTOs;

namespace EntertainmentTracker.Application.Animes.Interfaces
{
    public interface IAnimeService
    {
        Task<IReadOnlyList<AnimeSearchResponse>> SearchAsync(
            string query,
            CancellationToken cancellationToken = default);

        Task<AnimeDetailsResponse> GetDetailsAsync(
            int malId,
            CancellationToken cancellationToken = default);
    }
}
