
using System.Text.Json.Serialization;

namespace EntertainmentTracker.Infrastructure.External.Jikan.Models
{
    public sealed class JikanAnimeResponse
    {
        [JsonPropertyName("data")]
        public JikanAnimeDetails Data { get; set; } = null!;
    }

    public sealed class JikanAnimeDetails
    {
        [JsonPropertyName("mal_id")]
        public int MalId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("title_english")]
        public string? TitleEnglish { get; set; }

        [JsonPropertyName("synopsis")]
        public string? Synopsis { get; set; }

        [JsonPropertyName("episodes")]
        public int? Episodes { get; set; }

        [JsonPropertyName("score")]
        public double? Score { get; set; }

        [JsonPropertyName("rating")]
        public string? Rating { get; set; }

        [JsonPropertyName("season")]
        public string? Season { get; set; }

        [JsonPropertyName("year")]
        public int? Year { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("images")]
        public JikanImages Images { get; set; } = null!;

        [JsonPropertyName("trailer")]
        public JikanTrailer? Trailer { get; set; }

        [JsonPropertyName("genres")]
        public List<JikanGenreItem> Genres { get; set; } = [];
    }

    public sealed class JikanTrailer
    {
        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }

    public sealed class JikanGenreItem
    {
        [JsonPropertyName("mal_id")]
        public int MalId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
