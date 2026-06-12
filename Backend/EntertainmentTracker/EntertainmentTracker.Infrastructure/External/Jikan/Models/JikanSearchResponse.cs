using System.Text.Json.Serialization;

namespace EntertainmentTracker.Infrastructure.External.Jikan.Models
{
    public sealed class JikanSearchResponse
    {
        [JsonPropertyName("data")]
        public List<JikanAnimeItem> Data { get; set; } = [];
    }

    public sealed class JikanAnimeItem
    {
        [JsonPropertyName("mal_id")]
        public int MalId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("score")]
        public double? Score { get; set; }

        [JsonPropertyName("images")]
        public JikanImages Images { get; set; } = null!;
    }

    public sealed class JikanImages
    {
        [JsonPropertyName("jpg")]
        public JikanJpg Jpg { get; set; } = null!;
    }

    public sealed class JikanJpg
    {
        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }
    }
}
