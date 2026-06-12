
namespace EntertainmentTracker.Application.Animes.DTOs
{
    public sealed class AnimeSearchResponse
    {
        public int MalId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public double? Score { get; set; }
    }
}
