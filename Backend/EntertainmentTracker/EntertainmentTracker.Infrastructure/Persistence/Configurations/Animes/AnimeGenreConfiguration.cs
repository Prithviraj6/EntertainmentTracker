using EntertainmentTracker.Domain.Anime;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntertainmentTracker.Infrastructure.Persistence.Configurations.Animes
{
    public sealed class AnimeGenreConfiguration
    : IEntityTypeConfiguration<AnimeGenre>
    {
        public void Configure(
            EntityTypeBuilder<AnimeGenre> builder)
        {
            builder.ToTable("anime_genres");

            builder.HasKey(x =>
                new
                {
                    x.AnimeId,
                    x.GenreId
                });

            builder.HasOne(x => x.Anime)
                .WithMany(x => x.AnimeGenres)
                .HasForeignKey(x => x.AnimeId);

            builder.HasOne(x => x.Genre)
                .WithMany(x => x.AnimeGenres)
                .HasForeignKey(x => x.GenreId);
        }
    }
}
