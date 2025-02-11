using Microsoft.EntityFrameworkCore;
using Tawlity_Backend.Data;
using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;
using Tawlity_Backend.Services.Interface;
using Tawlity_Backend.SomeThingsWeWillUseInTheFuther;

namespace Tawlity_Backend.Services.Repo
{
    public class Login_Repo : Login_IRepo
    {
        private readonly AppDbContext _context;

        public Login_Repo(AppDbContext context)

        {
            _context = context;
        }

        public async Task<User?> GetEmployeeByEmailAsync(string email)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeEmail == email);
        }
        // New method to add an employee
        public async Task AddEmployeeAsync(User employee)
        {
            // Add the employee to the database context
            await _context.Employees.AddAsync(employee);

            // Save changes to the database
            await _context.SaveChangesAsync();
        }
        public async Task UpdateEmployeeAsync(User employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
        public async Task<User?> GetEmployeeByResetTokenAsync(string token)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.ResetToken == token);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByResetTokenAsync(string token)
        {
            return await _context.Employees.FirstOrDefaultAsync(u => u.ResetToken == token && u.ResetTokenExpiry > DateTime.UtcNow);
        }
    }
}