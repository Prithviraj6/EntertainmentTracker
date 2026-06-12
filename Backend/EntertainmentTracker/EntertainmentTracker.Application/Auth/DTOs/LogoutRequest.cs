
namespace EntertainmentTracker.Application.Auth.DTOs
{
    public sealed class LogoutRequest
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
