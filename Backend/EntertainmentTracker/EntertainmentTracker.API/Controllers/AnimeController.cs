using EntertainmentTracker.Application.Animes.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EntertainmentTracker.API.Controllers
{
    [ApiController]
    [Route("api/anime")]
    [Produces("application/json")]
    public sealed class AnimeController : ControllerBase
    {
        private readonly IAnimeService _animeService;

        public AnimeController(
            IAnimeService animeService)
        {
            _animeService = animeService;
        }

        /// <summary>
        /// Search anime using Jikan API.
        /// </summary>

        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string q,
            CancellationToken cancellationToken)
        {
            var result =
                await _animeService.SearchAsync(
                    q,
                    cancellationToken);

            return Ok(result);
        }

        [HttpGet("{malId:int}")]
        public async Task<IActionResult> Get(
            int malId,
            CancellationToken cancellationToken)
        {
            var result =
                await _animeService.GetDetailsAsync(
                    malId,
                    cancellationToken);

            return Ok(result);
        }
    }
}
