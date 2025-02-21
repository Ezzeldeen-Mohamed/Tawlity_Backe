using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;

namespace Tawlity_Backend.Services.IService
{
    public interface IReservationService
    {
        Task<bool> DeleteReservationAsync(int id);
        Task<bool> UpdateReservationAsync(int id, UpdateReservationDto updatedReservationDto);
        Task<ReservationResponseDto?> GetReservationByIdAsync(int id);
        Task<IEnumerable<ReservationResponseDto>> GetReservationsByUserIdAsync(int userId);
        Task<IEnumerable<ReservationResponseDto>> GetAllReservationsAsync();
        Task<bool> AddReservationAsync(ReservationDto reservationDto);

    }

}
