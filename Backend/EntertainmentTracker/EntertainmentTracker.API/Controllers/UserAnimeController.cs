using EntertainmentTracker.Application.Animes.DTOs;
using EntertainmentTracker.Application.Animes.Interfaces;
using EntertainmentTracker.Domain.Enums;
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
            [FromQuery] UserAnimeStatus? status,
            CancellationToken cancellationToken)
        {
            var userId =
                Guid.Parse(
                    User.FindFirstValue(
                        ClaimTypes.NameIdentifier)!);

            var result =
                await _userAnimeService.GetByUserAsync(
                    userId,
                    status,
                    cancellationToken);

            return Ok(result);
        }

        [HttpPatch("{animeId:guid}/progress")]
        public async Task<IActionResult> UpdateProgress(
            Guid animeId,
            UpdateProgressRequest request,
            CancellationToken cancellationToken)
        {
            var userId =
                Guid.Parse(
                    User.FindFirstValue(
                        ClaimTypes.NameIdentifier)!);

            await _userAnimeService.UpdateProgressAsync(
                userId,
                animeId,
                request,
                cancellationToken);

            return NoContent();
        }

        [HttpPatch("{animeId:guid}/status")]
        public async Task<IActionResult> UpdateStatus(
            Guid animeId,
            UpdateStatusRequest request,
            CancellationToken cancellationToken)
        {
            var userId =
                Guid.Parse(
                    User.FindFirstValue(
                        ClaimTypes.NameIdentifier)!);

            await _userAnimeService.UpdateStatusAsync(
                userId,
                animeId,
                request,
                cancellationToken);

            return NoContent();
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats(
            CancellationToken cancellationToken)
        {
            var userId =
                Guid.Parse(
                    User.FindFirstValue(
                        ClaimTypes.NameIdentifier)!);

            var result =
                await _userAnimeService.GetStatsAsync(
                    userId,
                    cancellationToken);

            return Ok(result);
        }
    }
}
