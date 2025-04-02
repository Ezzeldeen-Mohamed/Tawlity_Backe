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

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Employees.ToListAsync();
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeEmail == email);
        }
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }
        public async Task<User?> GetUserProfileByIdAsync(int id)
        {
            return await _context.Employees
                .Include(x=>x.Reservations)
                .ThenInclude(x=>x.Restaurant)
                .FirstOrDefaultAsync(x=>x.EmployeeId==id);
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Employees.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Employees.Update(user);
            await _context.SaveChangesAsync();
        }

        public void DeleteUser(User user)
        {
            _context.Employees.Remove(user);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
