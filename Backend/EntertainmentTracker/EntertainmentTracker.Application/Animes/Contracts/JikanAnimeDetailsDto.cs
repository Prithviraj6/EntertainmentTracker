
namespace EntertainmentTracker.Application.Animes
{
    public sealed class JikanAnimeDetailsDto
    {
        public int MalId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? TitleEnglish { get; set; }

        public string? Synopsis { get; set; }

        public int? Episodes { get; set; }

        public string? Rating { get; set; }

        public double? Score { get; set; }

        public string? Season { get; set; }

        public int? Year { get; set; }

        public string? ImageUrl { get; set; }

        public string? TrailerUrl { get; set; }

        public string? Status { get; set; }

        public List<JikanGenreDto> Genres { get; set; } = [];
    }
}
