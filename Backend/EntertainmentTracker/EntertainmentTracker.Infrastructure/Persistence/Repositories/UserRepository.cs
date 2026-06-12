using EntertainmentTracker.Application.Abstractions.Persistence;
using EntertainmentTracker.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace EntertainmentTracker.Infrastructure.Persistence.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(
            string email,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(
                    x => x.Email == email && !x.IsDeleted,
                    cancellationToken);
        }

        public async Task<bool> EmailExistsAsync(
            string email,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users
                .AnyAsync(
                    x => x.Email == email && !x.IsDeleted,
                    cancellationToken);
        }

        public async Task<bool> HandleExistsAsync(
            string handle,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users
                .AnyAsync(
                    x => x.Handle == handle,
                    cancellationToken);
        }

        public async Task AddAsync(
            User user,
            CancellationToken cancellationToken = default)
        {
            await _dbContext.Users.AddAsync(
                user,
                cancellationToken);
        }
    }
}
