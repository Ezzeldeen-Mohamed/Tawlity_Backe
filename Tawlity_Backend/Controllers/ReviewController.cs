using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tawlity_Backend.Dtos;
using Tawlity_Backend.Services.IService;

namespace Tawlity_Backend.Controllers
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

        // 🔹 GET: /api/reviews/restaurant/{id}
        [HttpGet("restaurant/{id}")]
        public async Task<IActionResult> GetReviewsByRestaurant(int id)
        {
            var reviews = await _reviewService.GetReviewsByRestaurantIdAsync(id);
            return Ok(reviews);
        }

        // 🔹 POST: /api/reviews
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SubmitReview([FromBody] CreateReviewDto reviewDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _reviewService.AddReviewAsync(reviewDto);
            return result ? Ok(new { message = "Review submitted successfully." }) : BadRequest(new { message = "Failed to submit review." });
        }

        // 🔹 DELETE: /api/reviews/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,RestaurantOwner")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var result = await _reviewService.DeleteReviewAsync(id);
            return result ? Ok(new { message = "Review deleted successfully." }) : NotFound();
        }
    }
}
