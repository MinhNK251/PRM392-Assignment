using AirWaterStore.API.DTOs;
using AirWaterStore.Business.Interfaces;
using AirWaterStore.Business.Services;
using AirWaterStore.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirWaterStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET: api/review/game/5
        [HttpGet("game/{gameId}")]
        public async Task<IActionResult> GetAllByGameIdAsync(int gameId)
        {
            var reviews = await _reviewService.GetAllByGameIdAsync(gameId);
            return Ok(reviews);
        }

        // POST: api/review
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateReviewDto dto)
        {
            var review = new Review
            {
                UserId = dto.UserId,
                GameId = dto.GameId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                ReviewDate = DateTime.Now
            };
            await _reviewService.AddAsync(review);
            return Ok(review);
        }

        // PUT: api/review/5
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateReviewDto dto)
        {
            var existing = await _reviewService.GetByIdAsync(dto.ReviewId);
            if (existing == null)
                return NotFound();
            if (dto.UserId != existing.UserId)
                return BadRequest("You cannot update this review");
            existing.Rating = dto.Rating;
            existing.Comment = dto.Comment;
            await _reviewService.UpdateAsync(existing);
            return NoContent();
        }

        // DELETE: api/review/5
        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteAsync(int reviewId)
        {
            await _reviewService.DeleteAsync(reviewId);
            return NoContent();
        }
    }
}
