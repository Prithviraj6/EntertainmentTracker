using EntertainmentTracker.Application.Abstractions.Persistence;
using EntertainmentTracker.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace EntertainmentTracker.Infrastructure.Persistence.Repositories
{
    public sealed class RefreshTokenRepository
    : IRefreshTokenRepository
    {
        private readonly AppDbContext _dbContext;

        public RefreshTokenRepository(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RefreshToken?> GetByTokenAsync(
            string token,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.RefreshTokens
                .Include(x => x.User)
                .FirstOrDefaultAsync(
                    x => x.TokenHash == token,
                    cancellationToken);
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
