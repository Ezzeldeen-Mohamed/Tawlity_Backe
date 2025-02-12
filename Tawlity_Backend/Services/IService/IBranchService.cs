using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;

namespace Tawlity_Backend.Services.IService
{
    public interface IBranchService
    {
        Task<IEnumerable<BranchDto>> GetBranchesByRestaurantIdAsync(int restaurantId);
        Task<BranchDto?> GetBranchByIdAsync(int branchId);
        Task AddBranchAsync(CreateBranchDto branchDto);
        Task DeleteBranchAsync(int branchId);
    }
}
