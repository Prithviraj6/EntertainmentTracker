
namespace EntertainmentTracker.Application.Auth.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateAccessToken(Guid userId);
    }
}
