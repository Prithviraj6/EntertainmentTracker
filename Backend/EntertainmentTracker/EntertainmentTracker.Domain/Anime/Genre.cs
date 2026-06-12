using EntertainmentTracker.Domain.Common;

namespace EntertainmentTracker.Domain.Anime
{
    public sealed class Genre : BaseEntity
    {
        public int MalId { get; private set; }

        public string Name { get; private set; } = null!;

        public ICollection<AnimeGenre> AnimeGenres { get; private set; }
            = new List<AnimeGenre>();

        private Genre()
        {
        }

        public static Genre Create(
            int malId,
            string name)
        {
            return new Genre
            {
                MalId = malId,
                Name = name
            };
        }
    }
}
