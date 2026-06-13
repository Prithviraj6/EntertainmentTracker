using EntertainmentTracker.Application.Animes.DTOs;
using EntertainmentTracker.Application.Animes.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EntertainmentTracker.API.Controllers
{
    [ApiController]
    [Route("api/user-anime")]
    [Authorize]
    public sealed class UserAnimeController : ControllerBase
    {
        private readonly IUserAnimeService _userAnimeService;

        public UserAnimeController(
            IUserAnimeService userAnimeService)
        {
            _userAnimeService = userAnimeService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(
            AddUserAnimeRequest request,
            CancellationToken cancellationToken)
        {
            var userId =
                Guid.Parse(
                    User.FindFirstValue(
                        ClaimTypes.NameIdentifier)!);

            var result =
                await _userAnimeService.AddAsync(
                    userId,
                    request,
                    cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            CancellationToken cancellationToken)
        {
            var userId =
                Guid.Parse(
                    User.FindFirstValue(
                        ClaimTypes.NameIdentifier)!);

            var result =
                await _userAnimeService.GetByUserAsync(
                    userId,
                    cancellationToken);

            return Ok(result);
        }
    }
}
