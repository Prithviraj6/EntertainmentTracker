using EntertainmentTracker.Application.Auth.DTOs;
using EntertainmentTracker.Application.Auth.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EntertainmentTracker.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Produces("application/json")]
    public sealed class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(
            IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(
            RegisterRequest request,
            CancellationToken cancellationToken)
        {
            var response =
                await _authService.RegisterAsync(
                    request,
                    cancellationToken);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(
            LoginRequest request,
            CancellationToken cancellationToken)
        {
            var response =
                await _authService.LoginAsync(
                    request,
                    cancellationToken);

            return Ok(response);
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var userId =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier)
                ??
                User.FindFirstValue("sub");

            var email =
                User.FindFirstValue(
                    ClaimTypes.Email);

            var handle =
                User.FindFirstValue(
                    "handle");

            return Ok(new
            {
                UserId = userId,
                Email = email,
                Handle = handle
            });
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<AuthResponse>> Refresh(
            RefreshRequest request,
            CancellationToken cancellationToken)
        {
            var response =
                await _authService.RefreshAsync(
                    request,
                    cancellationToken);

            return Ok(response);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(
            LogoutRequest request,
            CancellationToken cancellationToken)
        {
            await _authService.LogoutAsync(
                request,
                cancellationToken);

            return NoContent();
        }
    }
}
