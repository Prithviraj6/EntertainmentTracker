using EntertainmentTracker.Application.Auth.Interfaces;
using System.Security.Cryptography;

namespace EntertainmentTracker.Infrastructure.Security
{
    public sealed class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        public string Generate()
        {
            return Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64));
        }
    }
}
