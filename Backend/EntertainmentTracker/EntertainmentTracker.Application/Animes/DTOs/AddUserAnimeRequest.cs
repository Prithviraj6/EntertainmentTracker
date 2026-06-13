using EntertainmentTracker.Domain.Enums;

namespace EntertainmentTracker.Application.Animes.DTOs
{
    public sealed class AddUserAnimeRequest
    {
        public Guid AnimeId { get; set; }

        public UserAnimeStatus Status { get; set; }
    }
}
