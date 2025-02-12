using Microsoft.EntityFrameworkCore;
using Tawlity_Backend.Data;
using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;

namespace Tawlity_Backend.Repositories.Repositories
{
    public class BranchRepository:IBranchRepository
    {
        private readonly AppDbContext _context;

        public BranchRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Branch>> GetBranchesByRestaurantIdAsync(int restaurantId)
        {
            return await _context.Branches
                .Where(b => b.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task<Branch?> GetBranchByIdAsync(int branchId)
        {
            return await _context.Branches.FindAsync(branchId);
        }

        public async Task AddBranchAsync(Branch branch)
        {
            await _context.Branches.AddAsync(branch);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBranchAsync(int branchId)
        {
            var branch = await _context.Branches.FindAsync(branchId);
            if (branch != null)
            {
                _context.Branches.Remove(branch);
                await _context.SaveChangesAsync();
            }
        }
    }
}
