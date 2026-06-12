using EntertainmentTracker.Domain.Common;
using EntertainmentTracker.Domain.Enums;

namespace EntertainmentTracker.Domain.Anime
{
    public sealed class Anime : BaseEntity
    {
        public int MalId { get; private set; }

        public string Title { get; private set; } = null!;

        public string? TitleEnglish { get; private set; }

        public string? Synopsis { get; private set; }

        public int? Episodes { get; private set; }

        public AnimeStatus Status { get; private set; }

        public string? Rating { get; private set; }

        public double? Score { get; private set; }

        public string? Season { get; private set; }

        public int? Year { get; private set; }

        public string? ImageUrl { get; private set; }

        public string? TrailerUrl { get; private set; }

        public ICollection<AnimeGenre> AnimeGenres { get; private set; }
            = new List<AnimeGenre>();

        private Anime()
        {
        }

        public static Anime Create(
            int malId,
            string title,
            string? titleEnglish,
            string? synopsis,
            int? episodes,
            AnimeStatus status,
            string? rating,
            double? score,
            string? season,
            int? year,
            string? imageUrl,
            string? trailerUrl)
        {
            return new Anime
            {
                MalId = malId,
                Title = title,
                TitleEnglish = titleEnglish,
                Synopsis = synopsis,
                Episodes = episodes,
                Status = status,
                Rating = rating,
                Score = score,
                Season = season,
                Year = year,
                ImageUrl = imageUrl,
                TrailerUrl = trailerUrl
            };
        }
    }
}
