using EntertainmentTracker.Domain.Users;

namespace EntertainmentTracker.Application.Abstractions.Persistence
{
    public interface IRefreshTokenRepository
    {
       Task AddAsync(
       RefreshToken refreshToken,
       CancellationToken cancellationToken = default);
    }
}
