using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;
using Tawlity_Backend.Services.IService;

namespace Tawlity_Backend.Services.Service
{

    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            return await _reservationRepository.GetAllReservationsAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(int userId)
        {
            return await _reservationRepository.GetReservationsByUserIdAsync(userId);
        }

        public async Task<Reservation?> GetReservationByIdAsync(int id)
        {
            return await _reservationRepository.GetReservationByIdAsync(id);
        }

        public async Task AddReservationAsync(Reservation reservation)
        {
            await _reservationRepository.AddReservationAsync(reservation);
        }

        public async Task<bool> UpdateReservationAsync(int id, Reservation updatedReservation)
        {
            var existingReservation = await _reservationRepository.GetReservationByIdAsync(id);
            if (existingReservation == null) return false;

            existingReservation.ReservationTime = updatedReservation.ReservationTime;
            existingReservation.ReservationDate = updatedReservation.ReservationDate;
            existingReservation.PeopleCount = updatedReservation.PeopleCount;
            existingReservation.Status = updatedReservation.Status;

            await _reservationRepository.UpdateReservationAsync(existingReservation);
            return true;
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            var existingReservation = await _reservationRepository.GetReservationByIdAsync(id);
            if (existingReservation == null) return false;

            await _reservationRepository.DeleteReservationAsync(id);
            return true;
        }
    }
}
