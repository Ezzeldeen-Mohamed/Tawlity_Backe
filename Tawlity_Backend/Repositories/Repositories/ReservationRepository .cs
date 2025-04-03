using Microsoft.EntityFrameworkCore;
using Tawlity_Backend.Data;
using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;

namespace Tawlity_Backend.Repositories.Repositories
{

    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            return await _context.Reservations
                .Include(r => r.User)
                .ThenInclude(t => t.Restaurants)
                .Include(r => r.Table)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(int userId)
        {
            return await _context.Reservations
                .Where(r => r.UserId == userId)
                .Include(r => r.Table)
                .ToListAsync();
        }
        public async Task<bool> TableIsReservedAsync(int tableId, DateTime date, TimeSpan time)
        {
            return await _context.Reservations.AnyAsync(r =>
                r.TableId == tableId &&
                r.ReservationDate.Date == date.Date && // تأكد من مقارنة التواريخ فقط
                r.ReservationTime == time); // مقارنة الوقت المحجوز
        }
        public async Task<Reservation?> GetReservationByIdAsync(int id)
        {
            return await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Table)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddReservationAsync(Reservation reservation)
        {
            if (reservation.ReservationDate == default)
                throw new Exception("Invalid ReservationDate.");
            if (reservation.ReservationTime == default)
                throw new Exception("Invalid ReservationTime.");
            if (await _context.Tables.FindAsync(reservation.TableId) == null)
                throw new Exception("Table does not exist.");
            if (await _context.Employees.FindAsync(reservation.UserId) == null)
                throw new Exception("User does not exist.");
            if (await _context.Restaurants.FindAsync(reservation.RestaurantId) == null)
                throw new Exception("Restaurant does not exist.");

            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReservationAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReservationAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
