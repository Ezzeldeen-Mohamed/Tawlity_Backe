using Tawlity_Backend.Dtos;

namespace Tawlity_Backend.Services.IService
{
    public interface ICommunityPostService
    {
        Task<IEnumerable<PostResponseDto>> GetAllPostsAsync();
        Task<bool> CreatePostAsync(CreatePostDto postDto);
    }
}
