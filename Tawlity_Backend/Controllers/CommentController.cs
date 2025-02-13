using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tawlity_Backend.Dtos;
using Tawlity_Backend.Services.IService;

namespace Tawlity_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // 🔹 POST: /api/comments (Add a new comment)
        [HttpPost]
        [Authorize] // Ensure only authenticated users can comment
        public async Task<IActionResult> AddComment([FromBody] CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _commentService.AddCommentAsync(commentDto);
            if (!success) return NotFound("User or post not found.");

            return Ok(new { message = "Comment added successfully." });
        }
    }
}
