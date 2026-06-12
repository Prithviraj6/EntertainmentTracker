using EntertainmentTracker.Domain.Anime;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntertainmentTracker.Infrastructure.Persistence.Configurations.Animes
{
    public sealed class AnimeConfiguration
    : IEntityTypeConfiguration<Anime>
    {
        public void Configure(
            EntityTypeBuilder<Anime> builder)
        {
            builder.ToTable("animes");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.MalId)
                .IsUnique();

            builder.Property(x => x.Title)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.TitleEnglish)
                .HasMaxLength(500);

            builder.Property(x => x.Rating)
                .HasMaxLength(50);

            builder.Property(x => x.Season)
                .HasMaxLength(50);

            builder.Property(x => x.ImageUrl)
                .HasMaxLength(2000);

            builder.Property(x => x.TrailerUrl)
                .HasMaxLength(2000);
        }
    }
}
