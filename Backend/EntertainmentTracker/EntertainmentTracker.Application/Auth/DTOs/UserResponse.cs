
namespace EntertainmentTracker.Application.Auth.DTOs
{
    public sealed class UserResponse
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string Handle { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsEmailVerified { get; set; }
    }
}
