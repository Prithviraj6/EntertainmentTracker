
namespace EntertainmentTracker.Domain.Animes
{
    public sealed class AnimeGenre
    {
        public Guid AnimeId { get; private set; }

        public Anime Anime { get; private set; } = null!;

        public Guid GenreId { get; private set; }

        public Genre Genre { get; private set; } = null!;

        private AnimeGenre()
        {
        }

        public static AnimeGenre Create(
            Anime anime,
            Genre genre)
        {
            return new AnimeGenre
            {
                Anime = anime,
                Genre = genre
            };
        }
    }
}
