using EntertainmentTracker.Domain.Enums;

namespace EntertainmentTracker.Application.Animes.DTOs
{
    public sealed class UserAnimeDetailsResponse
    {
        public Guid AnimeId { get; set; }

        public string AnimeTitle { get; set; } = string.Empty;

        public UserAnimeStatus Status { get; set; }

        public int Progress { get; set; }

        public int? UserScore { get; set; }

        public DateTime? StartedAtUtc { get; set; }

        public DateTime? CompletedAtUtc { get; set; }
    }
}
