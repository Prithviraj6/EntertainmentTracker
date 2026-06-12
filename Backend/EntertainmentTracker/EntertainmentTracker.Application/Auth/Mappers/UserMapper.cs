using EntertainmentTracker.Application.Auth.DTOs;
using EntertainmentTracker.Domain.Users;

namespace EntertainmentTracker.Application.Auth.Mappers
{
    public static class UserMapper
    {
        public static UserResponse ToResponse(
            User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Handle = user.Handle,
                Email = user.Email,
                IsEmailVerified = user.IsEmailVerified
            };
        }
    }
}
