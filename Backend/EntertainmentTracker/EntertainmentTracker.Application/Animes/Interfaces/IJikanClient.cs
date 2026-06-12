using EntertainmentTracker.Application.Animes.DTOs;

namespace EntertainmentTracker.Application.Animes.Interfaces
{
    public interface IJikanClient
    {
        Task<IReadOnlyList<AnimeSearchResponse>> SearchAnimeAsync(
            string query,
            CancellationToken cancellationToken = default);

        Task<JikanAnimeDetailsDto?> GetAnimeAsync(
            int malId,
            CancellationToken cancellationToken = default);
    }
}
