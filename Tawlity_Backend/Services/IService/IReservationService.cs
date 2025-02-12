using Tawlity_Backend.Models;

namespace Tawlity_Backend.Services.IService
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();
        Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(int userId);
        Task<Reservation?> GetReservationByIdAsync(int id);
        Task AddReservationAsync(Reservation reservation);
        Task<bool> UpdateReservationAsync(int id, Reservation updatedReservation);
        Task<bool> DeleteReservationAsync(int id);
    }
}
