using EntertainmentTracker.Domain.Common;

namespace EntertainmentTracker.Domain.Users;

public sealed class User : BaseEntity
{
    public string DisplayName { get; private set; } = null!;
    public string Handle { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public bool IsEmailVerified { get; private set; } 
    public DateTime? LastLoginAtUtc { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAtUtc { get; private set; }

    public ICollection<RefreshToken> RefreshTokens { get; private set; } 
        = new List<RefreshToken>();

    private User() { }

    public static User Create(
        string displayName,
        string handle,
        string email,
        string passwordHash)
    {
        return new User
        {
            DisplayName = displayName,
            Handle = handle,
            Email = email,
            PasswordHash = passwordHash,

            IsEmailVerified = false,
            IsDeleted = false,
        };
    }

    public void MarkEmailVerified()
    {
        if (IsEmailVerified)
            return;

        IsEmailVerified = true;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void UpdateLastLogin()
    {
        LastLoginAtUtc = DateTime.UtcNow;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void Delete()
    {
        if (IsDeleted)
            return;

        IsDeleted = true;
        DeletedAtUtc = DateTime.UtcNow;
        UpdatedAtUtc = DateTime.UtcNow;
    }
}
