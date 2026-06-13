using EntertainmentTracker.Application.Abstractions.Persistence;
using EntertainmentTracker.Application.Animes.DTOs;
using EntertainmentTracker.Application.Animes.Interfaces;
using EntertainmentTracker.Application.Common.Exceptions;
using EntertainmentTracker.Domain.Animes;

namespace EntertainmentTracker.Application.Animes.Services
{
    public sealed class UserAnimeService
    : IUserAnimeService
    {
        private readonly IUserAnimeRepository _userAnimeRepository;
        private readonly IAnimeRepository _animeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserAnimeService(
            IUserAnimeRepository userAnimeRepository,
            IAnimeRepository animeRepository,
            IUnitOfWork unitOfWork)
        {
            _userAnimeRepository = userAnimeRepository;
            _animeRepository = animeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserAnimeResponse> AddAsync(
            Guid userId,
            AddUserAnimeRequest request,
            CancellationToken cancellationToken = default)
        {
            var existing =
                await _userAnimeRepository.GetAsync(
                    userId,
                    request.AnimeId,
                    cancellationToken);

            if (existing is not null)
            {
                throw new ConflictException(
                    "Anime already exists in your list.");
            }

            var anime =
                await _animeRepository.GetByIdAsync(
                    request.AnimeId,
                    cancellationToken);

            if (anime is null)
            {
                throw new NotFoundException(
                    "Anime not found.");
            }

            var userAnime =
                UserAnime.Create(
                    userId,
                    request.AnimeId,
                    request.Status);

            await _userAnimeRepository.AddAsync(
                userAnime,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return new UserAnimeResponse
            {
                AnimeId = anime.Id,
                AnimeTitle = anime.Title,
                Status = userAnime.Status,
                Progress = userAnime.Progress,
                UserScore = userAnime.UserScore
            };
        }

        public async Task<IReadOnlyList<UserAnimeResponse>> GetByUserAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            var items =
                await _userAnimeRepository.GetByUserAsync(
                    userId,
                    cancellationToken);

            return items
                .Select(x => new UserAnimeResponse
                {
                    AnimeId = x.AnimeId,
                    AnimeTitle = x.Anime.Title,
                    Status = x.Status,
                    Progress = x.Progress,
                    UserScore = x.UserScore
                })
                .ToList();
        }
    }
}
