using EntertainmentTracker.Domain.Common;
using EntertainmentTracker.Domain.Enums;
using EntertainmentTracker.Domain.Users;

namespace EntertainmentTracker.Domain.Animes
{
    public sealed class UserAnime : BaseEntity
    {
        public Guid UserId { get; private set; }

        public User User { get; private set; } = null!;

        public Guid AnimeId { get; private set; }

        public Anime Anime { get; private set; } = null!;

        public UserAnimeStatus Status { get; private set; }

        public int Progress { get; private set; }

        public int? UserScore { get; private set; }

        public DateTime? StartedAtUtc { get; private set; }

        public DateTime? CompletedAtUtc { get; private set; }

        private UserAnime()
        {
        }

        public static UserAnime Create(
            Guid userId,
            Guid animeId,
            UserAnimeStatus status)
        {
            return new UserAnime
            {
                UserId = userId,
                AnimeId = animeId,
                Status = status,
                Progress = 0
            };
        }

        public void UpdateProgress(
            int progress)
        {
            Progress = progress;

            UpdatedAtUtc = DateTime.UtcNow;
        }

        public void UpdateStatus(
            UserAnimeStatus status)
        {
            Status = status;

            if (status == UserAnimeStatus.Watching &&
                StartedAtUtc is null)
            {
                StartedAtUtc = DateTime.UtcNow;
            }

            if (status == UserAnimeStatus.Completed)
            {
                CompletedAtUtc = DateTime.UtcNow;
            }

            UpdatedAtUtc = DateTime.UtcNow;
        }

        public void UpdateScore(
            int score)
        {
            UserScore = score;

            UpdatedAtUtc = DateTime.UtcNow;
        }
    }
}
