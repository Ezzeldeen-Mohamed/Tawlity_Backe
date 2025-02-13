using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tawlity_Backend.Dtos;
using Tawlity_Backend.Services.IService;

namespace Tawlity_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityPostController : ControllerBase
    {
        private readonly ICommunityPostService _postService;
        private readonly ICommentService _commentService;

        public CommunityPostController(ICommunityPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }

        // 🔹 GET: /api/posts
        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        // 🔹 POST: /api/posts (Authenticated Users Only)
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDto postDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _postService.CreatePostAsync(postDto);
            if (!success) return BadRequest(new { message = "Post creation failed." });

            return Ok(new { message = "Post created successfully!" });
        }

        // 🔹 POST: /api/posts/{postId}/comments (Authenticated Users Only)
        [HttpPost("{postId}/comments")]
        [Authorize]
        public async Task<IActionResult> AddComment(int postId, [FromBody] CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            commentDto.PostId = postId;
            var success = await _commentService.AddCommentAsync(commentDto);
            if (!success) return BadRequest(new { message = "Failed to add comment." });

            return Ok(new { message = "Comment added successfully!" });
        }
    }
}
