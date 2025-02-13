using Microsoft.EntityFrameworkCore;
using Tawlity_Backend.Data;
using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;

namespace Tawlity_Backend.Repositories.Repositories
{
    public class CommunityPostRepository : ICommunityPostRepository
    {
        private readonly AppDbContext _context;

        public CommunityPostRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CommunityPost>> GetAllPostsAsync()
        {
            return await _context.CommunityPosts.Include(p => p.User).ToListAsync();
        }

        public async Task<CommunityPost?> GetPostByIdAsync(int postId)
        {
            return await _context.CommunityPosts.FindAsync(postId);
        }

        public async Task AddPostAsync(CommunityPost post)
        {
            await _context.CommunityPosts.AddAsync(post);
            await _context.SaveChangesAsync();
        }
    }

}
