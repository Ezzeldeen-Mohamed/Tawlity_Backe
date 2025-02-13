using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;

namespace Tawlity_Backend.Services.IService
{
    public class CommunityPostService : ICommunityPostService
    {
        private readonly ICommunityPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public CommunityPostService(ICommunityPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<PostResponseDto>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            return posts.Select(p => new PostResponseDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                Likes = p.Likes,
                UserId = p.UserId,
                UserName = p.User.EmployeeName,
                CreatedAt = p.CreatedAt
            }).ToList();
        }

        public async Task<bool> CreatePostAsync(CreatePostDto postDto)
        {
            var user = await _userRepository.GetUserByIdAsync(postDto.UserId);
            if (user == null) return false;

            var post = new CommunityPost
            {
                Title = postDto.Title,
                Content = postDto.Content,
                UserId = postDto.UserId
            };

            await _postRepository.AddPostAsync(post);
            return true;
        }
    }
}
