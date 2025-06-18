using AirWaterStore.API.DTOs;
using AirWaterStore.Business.Interfaces;
using AirWaterStore.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirWaterStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET: api/Game?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var games = await _gameService.GetAllAsync(pageNumber, pageSize);
            var total = await _gameService.GetTotalCountAsync();

            return Ok(new
            {
                totalCount = total,
                data = games
            });
        }

        // GET: api/Game/5
        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetByIdAsync(int gameId)
        {
            var game = await _gameService.GetByIdAsync(gameId);
            if (game == null) return NotFound();
            return Ok(game);
        }

        // POST: api/Game
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateGameDto dto)
        {
            var game = new Game
            {
                Title = dto.Title,
                ThumbnailUrl = dto.ThumbnailUrl,
                Description = dto.Description,
                Genre = dto.Genre,
                Developer = dto.Developer,
                Publisher = dto.Publisher,
                ReleaseDate = dto.ReleaseDate,
                Price = dto.Price,
                Quantity = dto.Quantity
            };
            await _gameService.AddAsync(game);
            return CreatedAtAction(nameof(GetByIdAsync), new { gameId = game.GameId }, game);
        }

        // PUT: api/Game/5
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateGameDto dto)
        {
            var existing = await _gameService.GetByIdAsync(dto.GameId);
            if (existing == null)
                return NotFound();

            existing.Title = dto.Title;
            existing.ThumbnailUrl = dto.ThumbnailUrl;
            existing.Description = dto.Description;
            existing.Genre = dto.Genre;
            existing.Developer = dto.Developer;
            existing.Publisher = dto.Publisher;
            existing.ReleaseDate = dto.ReleaseDate;
            existing.Price = dto.Price;
            existing.Quantity = dto.Quantity;

            await _gameService.UpdateAsync(existing);
            return NoContent();
        }

        // DELETE: api/Game/5
        [HttpDelete("{gameId}")]
        public async Task<IActionResult> DeleteAsync(int gameId)
        {
            var existing = await _gameService.GetByIdAsync(gameId);
            if (existing == null)
                return NotFound();

            await _gameService.DeleteAsync(gameId);
            return NoContent();
        }
    }
}
