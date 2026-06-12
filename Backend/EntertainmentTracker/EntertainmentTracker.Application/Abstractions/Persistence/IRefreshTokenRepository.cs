using EntertainmentTracker.Domain.Users;

namespace EntertainmentTracker.Application.Abstractions.Persistence
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenAsync(
            string token,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            RefreshToken refreshToken,
            CancellationToken cancellationToken = default);
    }
}
