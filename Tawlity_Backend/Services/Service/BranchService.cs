using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;
using Tawlity_Backend.Services.IService;

namespace Tawlity_Backend.Services.Service
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;

        public BranchService(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task<IEnumerable<Branch>> GetBranchesByRestaurantIdAsync(int restaurantId)
        {
            return await _branchRepository.GetBranchesByRestaurantIdAsync(restaurantId);
        }

        public async Task<Branch?> GetBranchByIdAsync(int branchId)
        {
            return await _branchRepository.GetBranchByIdAsync(branchId);
        }

        public async Task AddBranchAsync(Branch branch)
        {
            await _branchRepository.AddBranchAsync(branch);
        }

        public async Task DeleteBranchAsync(int branchId)
        {
            await _branchRepository.DeleteBranchAsync(branchId);
        }
    }

}
