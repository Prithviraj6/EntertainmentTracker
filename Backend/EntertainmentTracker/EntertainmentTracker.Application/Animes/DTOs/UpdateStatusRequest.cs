using EntertainmentTracker.Domain.Enums;

namespace EntertainmentTracker.Application.Animes.DTOs
{
    public sealed class UpdateStatusRequest
    {
        public UserAnimeStatus Status { get; set; }
    }
}
