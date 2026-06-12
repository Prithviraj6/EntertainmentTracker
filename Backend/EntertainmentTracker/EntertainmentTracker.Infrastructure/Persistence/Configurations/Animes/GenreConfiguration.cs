using EntertainmentTracker.Domain.Animes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntertainmentTracker.Infrastructure.Persistence.Configurations.Animes
{
    public sealed class GenreConfiguration
    : IEntityTypeConfiguration<Genre>
    {
        public void Configure(
            EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("genres");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.MalId)
                .IsUnique();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
