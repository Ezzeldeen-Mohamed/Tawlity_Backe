using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Tawlity_Backend.Data;
using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;

namespace Tawlity_Backend.Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Employees.Include(u => u.Employee_Role)
                                       .FirstOrDefaultAsync(u => u.EmployeeId == id);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Employees.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Employees.FindAsync(id);
            if (user == null) return false;

            _context.Employees .Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Favorite>> GetUserFavoritesAsync(int userId)
        {
            return await _context.Favorites.Where(f => f.UserId == userId)
                                           .Include(f => f.Restaurant)
                                           .Include(f => f.MenuItem)
                                           .ToListAsync();
        }
    }

}
