using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;

namespace Tawlity_Backend.Services.IService
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationResponseDto>> GetAllReservationsAsync();
        Task<IEnumerable<ReservationResponseDto>> GetReservationsByUserIdAsync(int userId);
        Task<ReservationResponseDto?> GetReservationByIdAsync(int id);
        Task AddReservationAsync(int userId, ReservationDto reservationDto);
        Task<bool> UpdateReservationAsync(int id, UpdateReservationDto updatedReservationDto);
        Task<bool> DeleteReservationAsync(int id);
    }

}
