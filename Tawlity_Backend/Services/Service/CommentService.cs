using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;
using Tawlity_Backend.Services.IService;

namespace Tawlity_Backend.Services.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ICommunityPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public CommentService(ICommentRepository commentRepository, ICommunityPostRepository postRepository, IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> AddCommentAsync(CreateCommentDto commentDto)
        {
            var user = await _userRepository.GetUserByIdAsync(commentDto.UserId);
            var post = await _postRepository.GetPostByIdAsync(commentDto.PostId);
            if (user == null || post == null) return false;

            var comment = new Comment
            {
                Content = commentDto.Content,
                UserId = commentDto.UserId,
                PostId = commentDto.PostId,
                CreatedAt = DateTime.UtcNow
            };

            await _commentRepository.AddCommentAsync(comment);
            return true;
        }
    }
}
