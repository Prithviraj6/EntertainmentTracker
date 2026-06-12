
using EntertainmentTracker.Domain.Users;

namespace EntertainmentTracker.Application.Auth.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateAccessToken(User user);
        DateTime GetAccessTokenExpiryUtc();
    }
}
