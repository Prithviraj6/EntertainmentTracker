using EntertainmentTracker.Application.Abstractions.Persistence;
using EntertainmentTracker.Domain.Users;

namespace EntertainmentTracker.Infrastructure.Persistence.Repositories
{
    public sealed class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _dbContext;

        public RefreshTokenRepository(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(
            RefreshToken refreshToken,
            CancellationToken cancellationToken = default)
        {
            await _dbContext.RefreshTokens.AddAsync(
                refreshToken,
                cancellationToken);
        }
    }
}
