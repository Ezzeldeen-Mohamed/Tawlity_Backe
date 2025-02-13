using Tawlity_Backend.Models;

namespace Tawlity_Backend.Repositories.Interface
{
    public interface ICommunityPostRepository
    {
        Task<IEnumerable<CommunityPost>> GetAllPostsAsync();
        Task<CommunityPost?> GetPostByIdAsync(int postId);
        Task AddPostAsync(CommunityPost post);
    }
}
