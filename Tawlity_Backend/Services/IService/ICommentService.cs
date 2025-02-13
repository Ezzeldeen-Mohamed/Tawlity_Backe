using Tawlity_Backend.Dtos;

namespace Tawlity_Backend.Services.IService
{
    public interface ICommentService
    {
        Task<bool> AddCommentAsync(CreateCommentDto commentDto);
    }
}
