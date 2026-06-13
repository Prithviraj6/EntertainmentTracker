using EntertainmentTracker.Application.Abstractions.Persistence;
using EntertainmentTracker.Application.Auth.DTOs;
using EntertainmentTracker.Application.Auth.Interfaces;
using EntertainmentTracker.Application.Common.Constants;
using EntertainmentTracker.Application.Common.Exceptions;
using EntertainmentTracker.Application.Common.Interfaces;
using EntertainmentTracker.Application.Common.Security;
using EntertainmentTracker.Domain.Users;

namespace EntertainmentTracker.Application.Auth.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IDateTimeProvider _dateTimeProvider;

        public AuthService(
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider,
            IDateTimeProvider dateTimeProvider)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;

            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request,
            CancellationToken cancellationToken = default)
        {
            var displayName = request.DisplayName.Trim();

            var handle = request.Handle
                .Trim()
                .ToLowerInvariant();

            var email = request.Email
                .Trim()
                .ToLowerInvariant();

            if (await _userRepository.EmailExistsAsync(
                    email,
                    cancellationToken))
            {
                throw new ConflictException(
                    "Email already exists.");
            }

            if (await _userRepository.HandleExistsAsync(
                    handle,
                    cancellationToken))
            {
                throw new ConflictException(
                    "Handle already exists.");
            }

            var passwordHash =
                _passwordHasher.Hash(
                    request.Password);

            var user = User.Create(
                displayName,
                handle,
                email,
                passwordHash);

            await _userRepository.AddAsync(
                user,
                cancellationToken);

            var refreshToken = Convert.ToBase64String(
                System.Security.Cryptography.RandomNumberGenerator.GetBytes(64));

            var refreshTokenHash =
                TokenHasher.Hash(
                    refreshToken);

            var refreshTokenEntity =
                RefreshToken.Create(
                    user.Id,
                    refreshTokenHash,
                    _dateTimeProvider.UtcNow.AddDays(30));

            await _refreshTokenRepository.AddAsync(
                refreshTokenEntity,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            var accessToken =
                _jwtProvider.GenerateAccessToken(
                    user);

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,

                ExpiresAtUtc = _dateTimeProvider.UtcNow.AddMinutes(15),

                User = new UserResponse
                {
                    Id = user.Id,
                    DisplayName = user.DisplayName,
                    Handle = user.Handle,
                    Email = user.Email,
                    IsEmailVerified = user.IsEmailVerified
                }
            };
        }

        public async Task<AuthResponse> LoginAsync(
            LoginRequest request,
            CancellationToken cancellationToken = default)
        {
            var email = request.Email
                .Trim()
                .ToLowerInvariant();

            var user = await _userRepository.GetByEmailAsync(
                email,
                cancellationToken);

            if (user is null)
            {
                throw new UnauthorizedException(
                    "Invalid email or password.");
            }

            var isValidPassword =
                _passwordHasher.Verify(
                    request.Password,
                    user.PasswordHash);

            if (!isValidPassword)
            {
                throw new UnauthorizedException(
                    "Invalid email or password.");
            }

            user.UpdateLastLogin();

            var refreshToken = Convert.ToBase64String(
                System.Security.Cryptography.RandomNumberGenerator.GetBytes(64));

            var refreshTokenHash =
                TokenHasher.Hash(
                    refreshToken);

            var refreshTokenEntity =
                RefreshToken.Create(
                    user.Id,
                    refreshTokenHash,
                    _dateTimeProvider.UtcNow.AddDays(30));

            await _refreshTokenRepository.AddAsync(
                refreshTokenEntity,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            var accessToken =
                _jwtProvider.GenerateAccessToken(user);

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,

                ExpiresAtUtc = _dateTimeProvider.UtcNow.AddMinutes(15),

                User = new UserResponse
                {
                    Id = user.Id,
                    DisplayName = user.DisplayName,
                    Handle = user.Handle,
                    Email = user.Email,
                    IsEmailVerified = user.IsEmailVerified
                }
            };
        }

        public async Task<AuthResponse> RefreshAsync(
            RefreshRequest request,
            CancellationToken cancellationToken = default)
        {
            var refreshToken =
                await _refreshTokenRepository
                    .GetByTokenAsync(
                TokenHasher.Hash(
                    request.RefreshToken),
                cancellationToken);

            if (refreshToken is null)
            {
                throw new UnauthorizedException(
                    "Invalid refresh token.");
            }

            if (refreshToken.RevokedAtUtc is not null)
            {
                throw new UnauthorizedException(
                    "Refresh token has been revoked.");
            }

            if (refreshToken.ExpiresAtUtc <= _dateTimeProvider.UtcNow)
            {
                throw new UnauthorizedException(
                    "Refresh token has expired.");
            }

            refreshToken.Revoke();

            var newRefreshToken =
                Convert.ToBase64String(
                    System.Security.Cryptography
                        .RandomNumberGenerator.GetBytes(64));

            var newRefreshTokenHash =
                TokenHasher.Hash(
                    newRefreshToken);

            var newRefreshTokenEntity =
                RefreshToken.Create(
                    refreshToken.UserId,
                    newRefreshTokenHash,
                    _dateTimeProvider.UtcNow.AddDays(30));

            await _refreshTokenRepository.AddAsync(
                newRefreshTokenEntity,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            var accessToken =
                _jwtProvider.GenerateAccessToken(
                    refreshToken.User);

            return new AuthResponse
            {
                AccessToken = accessToken,

                RefreshToken = newRefreshToken,

                ExpiresAtUtc =
                    _dateTimeProvider.UtcNow.AddMinutes(15),

                User = new UserResponse
                {
                    Id = refreshToken.User.Id,
                    DisplayName =
                        refreshToken.User.DisplayName,
                    Handle =
                        refreshToken.User.Handle,
                    Email =
                        refreshToken.User.Email,
                    IsEmailVerified =
                        refreshToken.User.IsEmailVerified
                }
            };
        }

        public async Task LogoutAsync(
            LogoutRequest request,
            CancellationToken cancellationToken = default)
        {
            var refreshToken =
                await _refreshTokenRepository
                    .GetByTokenAsync(
                        TokenHasher.Hash(
                            request.RefreshToken),
                        cancellationToken);

            if (refreshToken is null)
            {
                return;
            }

            if (refreshToken.RevokedAtUtc is not null)
            {
                return;
            }

            refreshToken.Revoke();

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);
        }
    }
}
