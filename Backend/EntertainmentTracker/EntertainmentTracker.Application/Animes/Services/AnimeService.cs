using EntertainmentTracker.Application.Abstractions.Persistence;
using EntertainmentTracker.Application.Animes.DTOs;
using EntertainmentTracker.Domain.Enums;
using EntertainmentTracker.Application.Animes.Interfaces;
using EntertainmentTracker.Application.Common.Exceptions;
using EntertainmentTracker.Domain.Animes;


namespace EntertainmentTracker.Application.Animes.Services
{
    public sealed class AnimeService : IAnimeService
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJikanClient _jikanClient;

        public AnimeService(
        IAnimeRepository animeRepository,
        IGenreRepository genreRepository,
        IUnitOfWork unitOfWork,
        IJikanClient jikanClient)
        {
            _animeRepository = animeRepository;
            _genreRepository = genreRepository;
            _unitOfWork = unitOfWork;
            _jikanClient = jikanClient;
        }

        public async Task<IReadOnlyList<AnimeSearchResponse>>
            SearchAsync(
                string query,
                CancellationToken cancellationToken = default)
        {
            return await _jikanClient.SearchAnimeAsync(
                query,
                cancellationToken);
        }

        public async Task<AnimeDetailsResponse> GetDetailsAsync(
            int malId,
            CancellationToken cancellationToken = default)
        {
            var existingAnime =
                await _animeRepository.GetByMalIdAsync(
                    malId,
                    cancellationToken);

            if (existingAnime is not null)
            {
                return new AnimeDetailsResponse
                {
                    Id = existingAnime.Id,
                    MalId = existingAnime.MalId,
                    Title = existingAnime.Title,
                    TitleEnglish = existingAnime.TitleEnglish,
                    Synopsis = existingAnime.Synopsis,
                    Episodes = existingAnime.Episodes,
                    Rating = existingAnime.Rating,
                    Score = existingAnime.Score,
                    Season = existingAnime.Season,
                    Year = existingAnime.Year,
                    ImageUrl = existingAnime.ImageUrl,
                    TrailerUrl = existingAnime.TrailerUrl,

                    Genres = existingAnime.AnimeGenres
                        .Select(x => new GenreResponse
                        {
                            MalId = x.Genre.MalId,
                            Name = x.Genre.Name
                        })
                        .ToList()
                };
            }

            var animeFromJikan =
                await _jikanClient.GetAnimeAsync(
                    malId,
                    cancellationToken);

            if (animeFromJikan is null)
            {
                throw new NotFoundException(
                    "Anime not found.");
            }

            var anime = Anime.Create(
                animeFromJikan.MalId,
                animeFromJikan.Title,
                animeFromJikan.TitleEnglish,
                animeFromJikan.Synopsis,
                animeFromJikan.Episodes,
                MapStatus(animeFromJikan.Status),
                animeFromJikan.Rating,
                animeFromJikan.Score,
                animeFromJikan.Season,
                animeFromJikan.Year,
                animeFromJikan.ImageUrl,
                animeFromJikan.TrailerUrl);

            var genreMalIds =
                animeFromJikan.Genres
                    .Select(x => x.MalId)
                    .ToList();

            var existingGenres =
                await _genreRepository.GetByMalIdsAsync(
                    genreMalIds,
                    cancellationToken);

            var genresByMalId =
                existingGenres.ToDictionary(
                    x => x.MalId);

            foreach (var genreDto in animeFromJikan.Genres)
            {
                if (!genresByMalId.TryGetValue(
                        genreDto.MalId,
                        out var genre))
                {
                    genre = Genre.Create(
                        genreDto.MalId,
                        genreDto.Name);

                    await _genreRepository.AddAsync(
                        genre,
                        cancellationToken);

                    genresByMalId.Add(
                        genre.MalId,
                        genre);
                }

                anime.AddGenre(genre);
            }

            await _animeRepository.AddAsync(
                anime,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return new AnimeDetailsResponse
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
                ImageUrl = anime.ImageUrl,
                TrailerUrl = anime.TrailerUrl,

                Genres = animeFromJikan.Genres
                    .Select(x => new GenreResponse
                    {
                        MalId = x.MalId,
                        Name = x.Name
                    })
                    .ToList()
            };
        }

        private static AnimeStatus MapStatus(
            string? status)
        {
            return status switch
            {
                "Finished Airing" => AnimeStatus.FinishedAiring,
                "Currently Airing" => AnimeStatus.CurrentlyAiring,
                "Not yet aired" => AnimeStatus.NotYetAired,
                _ => AnimeStatus.Unknown
            };
        }
    }
}
