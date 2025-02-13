using Tawlity_Backend.Models;

namespace Tawlity_Backend.Repositories.Interface
{
    public interface ICommentRepository
    {
        Task AddCommentAsync(Comment comment);
    }
}
