
namespace EntertainmentTracker.Application.Auth.DTOs
{
    public sealed class RegisterRequest
    {
        public string DisplayName { get; set; } = string.Empty;
        public string Handle { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
