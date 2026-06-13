using EntertainmentTracker.Domain.Animes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntertainmentTracker.Infrastructure.Persistence.Configurations.Animes
{
    public sealed class UserAnimeConfiguration
    : IEntityTypeConfiguration<UserAnime>
    {
        public void Configure(
            EntityTypeBuilder<UserAnime> builder)
        {
            builder.ToTable("user_anime");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new
            {
                x.UserId,
                x.AnimeId
            }).IsUnique();

            builder.Property(x => x.Progress);

            builder.Property(x => x.UserScore);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Anime)
                .WithMany()
                .HasForeignKey(x => x.AnimeId);
        }
    }
}
