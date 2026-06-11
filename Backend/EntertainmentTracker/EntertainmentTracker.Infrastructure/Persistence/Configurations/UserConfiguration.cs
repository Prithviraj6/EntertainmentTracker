using EntertainmentTracker.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntertainmentTracker.Infrastructure.Persistence.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DisplayName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Handle)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(254)
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .HasMaxLength(512)
                .IsRequired();

            builder.Property(x => x.IsEmailVerified)
                .IsRequired();

            builder.Property(x => x.IsDeleted)
                .IsRequired();

            builder.HasIndex(x => x.Handle)
                .IsUnique();

            builder.HasMany(x => x.RefreshTokens)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
