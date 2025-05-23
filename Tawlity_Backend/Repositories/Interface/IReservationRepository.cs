﻿using Tawlity_Backend.Models;

namespace Tawlity_Backend.Repositories.Interface
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();
        Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(int userId);
        Task<Reservation?> GetReservationByIdAsync(int id);
        Task AddReservationAsync(Reservation reservation);
        Task UpdateReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(int id);
        Task<bool> TableIsReservedAsync(int tableId, DateTime date, TimeSpan time);

    }
}
