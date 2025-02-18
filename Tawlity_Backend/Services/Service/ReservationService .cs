using Tawlity_Backend.Data.Enums;
using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;
using Tawlity_Backend.Services.IService;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUserRepository _userRepository;
    private readonly EmailService _emailService; // Inject Email Service

    public ReservationService(IReservationRepository reservationRepository, IUserRepository userRepository, EmailService emailService)
    {
        _reservationRepository = reservationRepository;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<IEnumerable<ReservationResponseDto>> GetAllReservationsAsync()
    {
        var reservations = await _reservationRepository.GetAllReservationsAsync();
        return reservations.Select(r => new ReservationResponseDto
        {
            Id = r.Id,
            RestaurantId = r.RestaurantId,
            UserId = r.UserId,
            TableId = r.TableId,
            ReservationDate = r.ReservationDate,
            ReservationTime = r.ReservationTime,
            PeopleCount = r.PeopleCount,
            Status = r.Status.ToString()
        });
    }

    public async Task<IEnumerable<ReservationResponseDto>> GetReservationsByUserIdAsync(int userId)
    {
        var reservations = await _reservationRepository.GetReservationsByUserIdAsync(userId);
        return reservations.Select(r => new ReservationResponseDto
        {
            Id = r.Id,
            RestaurantId = r.RestaurantId,
            UserId = r.UserId,
            TableId = r.TableId,
            ReservationDate = r.ReservationDate,
            ReservationTime = r.ReservationTime,
            PeopleCount = r.PeopleCount,
            Status = r.Status.ToString()
        });
    }

    public async Task<ReservationResponseDto?> GetReservationByIdAsync(int id)
    {
        var reservation = await _reservationRepository.GetReservationByIdAsync(id);
        if (reservation == null) return null;

        return new ReservationResponseDto
        {
            Id = reservation.Id,
            RestaurantId = reservation.RestaurantId,
            UserId = reservation.UserId,
            TableId = reservation.TableId,
            ReservationDate = reservation.ReservationDate,
            ReservationTime = reservation.ReservationTime,
            PeopleCount = reservation.PeopleCount,
            Status = reservation.Status.ToString()
        };
    }

    public async Task<bool> AddReservationAsync(int userId, ReservationDto reservationDto)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
            throw new Exception("User not found.");

        var reservation = new Reservation
        {
            RestaurantId = reservationDto.RestaurantId,
            UserId = userId,
            TableId = reservationDto.TableId,
            ReservationDate = reservationDto.ReservationDate,
            ReservationTime = reservationDto.ReservationTime,
            PeopleCount = reservationDto.PeopleCount,
            OrderItems=reservationDto.OrderItems.Select(x=>new OrderItem
            {
                MenuItem=new MenuItem
                {
                    Name = x.Name,
                    Price = x.Price
                }
            }).ToList(),
            Status = reservationDto.Status
        };

         await _reservationRepository.AddReservationAsync(reservation);

        // Send confirmation email
        await _emailService.SendEmailAsync(user.EmployeeEmail, "Reservation Confirmation", $@"
        Dear {user.EmployeeName},  
        Your reservation at restaurant ID {reservationDto.RestaurantId} is confirmed.  
        📅 Date: {reservationDto.ReservationDate}  
        ⏰ Time: {reservationDto.ReservationTime}  
        👥 People: {reservationDto.PeopleCount}  
        Thank you for choosing our service!
    ");
        return true;
    }

    public async Task<bool> UpdateReservationAsync(int id, UpdateReservationDto updatedReservationDto)
    {
        var existingReservation = await _reservationRepository.GetReservationByIdAsync(id);
        if (existingReservation == null) return false;

        existingReservation.ReservationDate = updatedReservationDto.ReservationDate;
        existingReservation.ReservationTime = updatedReservationDto.ReservationTime;
        existingReservation.PeopleCount = updatedReservationDto.PeopleCount;
        // ✅ Convert String to Enum
        if (Enum.TryParse(updatedReservationDto.Status, out Reservation_Status status))
        {
            existingReservation.Status = status;
        }
        else
        {
            throw new Exception("Invalid reservation status provided.");
        }

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