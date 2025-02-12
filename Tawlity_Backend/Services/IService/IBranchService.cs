using Tawlity_Backend.Models;

namespace Tawlity_Backend.Services.IService
{
    public interface IBranchService
    {
        Task<IEnumerable<Branch>> GetBranchesByRestaurantIdAsync(int restaurantId);
        Task<Branch?> GetBranchByIdAsync(int branchId);
        Task AddBranchAsync(Branch branch);
        Task DeleteBranchAsync(int branchId);
    }
}
