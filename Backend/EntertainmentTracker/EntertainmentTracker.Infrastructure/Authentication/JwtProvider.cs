using EntertainmentTracker.Application.Auth.Interfaces;
using EntertainmentTracker.Application.Common.Constants;
using EntertainmentTracker.Application.Common.Interfaces;
using EntertainmentTracker.Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EntertainmentTracker.Infrastructure.Authentication
{
    public sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IDateTimeProvider _dateTimeProvider;

        public JwtProvider(
            IOptions<JwtOptions> jwtOptions,
            IDateTimeProvider dateTimeProvider)
        {
            _jwtOptions = jwtOptions.Value;
            _dateTimeProvider = dateTimeProvider;
        }

        public string GenerateAccessToken(User user)
        {
            var claims = new List<Claim>
        {
            new(
                JwtRegisteredClaimNames.Sub,
                user.Id.ToString()),

            new Claim(
                JwtRegisteredClaimNames.Email,
                user.Email),

            new(
                CustomClaimTypes.Handle,
                user.Handle)
        };

            var signingKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _jwtOptions.SecretKey));

            var credentials = new SigningCredentials(
                signingKey,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: _dateTimeProvider.UtcNow
                    .AddMinutes(
                        _jwtOptions.AccessTokenExpiryMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
