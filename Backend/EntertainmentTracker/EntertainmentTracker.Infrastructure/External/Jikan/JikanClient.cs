using EntertainmentTracker.Application.Animes;
using EntertainmentTracker.Application.Animes.DTOs;
using EntertainmentTracker.Application.Animes.Interfaces;
using EntertainmentTracker.Infrastructure.External.Jikan.Models;
using System.Net.Http.Json;

namespace EntertainmentTracker.Infrastructure.External.Jikan
{
    public sealed class JikanClient : IJikanClient
    {
        private readonly HttpClient _httpClient;

        public JikanClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyList<AnimeSearchResponse>>
            SearchAnimeAsync(
                string query,
                CancellationToken cancellationToken = default)
        {
            var response =
                await _httpClient.GetFromJsonAsync<
                    JikanSearchResponse>(
                    $"anime?q={Uri.EscapeDataString(query)}&limit=10",
                    cancellationToken);

            if (response is null)
            {
                return [];
            }

            return response.Data
                .Select(x => new AnimeSearchResponse
                {
                    MalId = x.MalId,
                    Title = x.Title,
                    Score = x.Score,
                    ImageUrl = x.Images?.Jpg?.ImageUrl
                })
                .ToList();
        }

        public async Task<JikanAnimeDetailsDto?> GetAnimeAsync(
            int malId,
            CancellationToken cancellationToken = default)
        {
            var response =
                await _httpClient.GetFromJsonAsync<
                    JikanAnimeResponse>(
                    $"anime/{malId}",
                    cancellationToken);

            if (response is null)
            {
                return null;
            }

            var anime = response.Data;

            return new JikanAnimeDetailsDto
            {
                MalId = anime.MalId,
                Title = anime.Title,
                TitleEnglish = anime.TitleEnglish,
                Synopsis = anime.Synopsis,
                Episodes = anime.Episodes,
                Rating = anime.Rating,
                Score = anime.Score,
                Season = anime.Season,
                Year = anime.Year,
                ImageUrl = anime.Images?.Jpg?.ImageUrl,
                TrailerUrl = anime.Trailer?.Url,

                Genres = anime.Genres
                .Select(x => new JikanGenreDto
                {
                    MalId = x.MalId,
                    Name = x.Name
                })
                .ToList()
            };
        }
    }
}
