using EntertainmentTracker.Application.Abstractions.Persistence;
using EntertainmentTracker.Application.Animes.DTOs;
using EntertainmentTracker.Application.Animes.Interfaces;
using EntertainmentTracker.Application.Common.Exceptions;
using EntertainmentTracker.Domain.Animes;
using EntertainmentTracker.Domain.Enums;

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
            UserAnimeStatus? status,
            CancellationToken cancellationToken = default)
        {
            var items =
                await _userAnimeRepository.GetByUserAsync(
                    userId,
                    status,
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

        public async Task UpdateProgressAsync(
            Guid userId,
            Guid animeId,
            UpdateProgressRequest request,
            CancellationToken cancellationToken = default)
        {
            var userAnime =
                await _userAnimeRepository.GetAsync(
                    userId,
                    animeId,
                    cancellationToken);

            if (userAnime is null)
            {
                throw new NotFoundException(
                    "Anime not found in user list.");
            }

            userAnime.UpdateProgress(
                request.Progress);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);
        }

        public async Task UpdateStatusAsync(
            Guid userId,
            Guid animeId,
            UpdateStatusRequest request,
            CancellationToken cancellationToken = default)
        {
            var userAnime =
                await _userAnimeRepository.GetAsync(
                    userId,
                    animeId,
                    cancellationToken);

            if (userAnime is null)
            {
                throw new NotFoundException(
                    "Anime not found in user list.");
            }

            userAnime.UpdateStatus(
                request.Status);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);
        }

        public async Task<UserAnimeStatsResponse> GetStatsAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            var items =
                await _userAnimeRepository.GetByUserAsync(
                    userId,
                    null,
                    cancellationToken);

            return new UserAnimeStatsResponse
            {
                PlanToWatch = items.Count(
                    x => x.Status == UserAnimeStatus.PlanToWatch),

                Watching = items.Count(
                    x => x.Status == UserAnimeStatus.Watching),

                Completed = items.Count(
                    x => x.Status == UserAnimeStatus.Completed),

                OnHold = items.Count(
                    x => x.Status == UserAnimeStatus.OnHold),

                Dropped = items.Count(
                    x => x.Status == UserAnimeStatus.Dropped)
            };
        }
    }
}
