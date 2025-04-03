using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.FileProviders;
using Tawlity_Backend.Data.Enums;
using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;
using Tawlity_Backend.Repositories.Repositories;
using Tawlity_Backend.Services.IService;

public class ReservationService : IReservationService
{

    private readonly IReservationRepository _reservationRepository;
    private readonly IRestaurantRepository _restaurantrepo;
    private readonly IMenuRepository _menuItemRepository;
    private readonly ITableRepository _tableRepository;
    private readonly IUserRepository _userRepository;
    private readonly EmailService _emailService; // Inject Email Service
    private readonly ILogger<ReservationService> _logger;

    public ReservationService(IRestaurantRepository restaurantrepo ,IReservationRepository reservationRepository,ITableRepository tableRepository,IMenuRepository menuItemRepository, ILogger<ReservationService> logger, IMenuService menuRepository, IUserRepository userRepository, EmailService emailService)
    {
        _restaurantrepo = restaurantrepo;
        _menuItemRepository = menuItemRepository;
        _logger = logger; 
        _reservationRepository = reservationRepository;
        _userRepository = userRepository;
        _tableRepository = tableRepository;
        _emailService = emailService;
    }
    public async Task<IEnumerable<ReservationResponseDto>> GetAllReservationsAsync()
    {
        var reservations = await _reservationRepository.GetAllReservationsAsync();
        return reservations?.Select(r => new ReservationResponseDto
        {
            Id = r.Id,
            RestaurantId = r.RestaurantId,
            UserId = r.UserId,
            TableId = r.TableId,
            ReservationDate = r.ReservationDate.ToString(),
            ReservationTime = r.ReservationTime.ToString(),
            PeopleCount = r.PeopleCount,
            Status = r.Status.ToString()
        }) ?? new List<ReservationResponseDto>();
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
            ReservationDate = r.ReservationDate.ToString("yyyy-MM-dd"), // ✅ تأكد من تحويل DateTime إلى string
            ReservationTime = r.ReservationTime.ToString(@"hh\:mm"), // ✅ تأكد من تحويل TimeSpan إلى string
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
            ReservationDate = reservation.ReservationDate.ToString(),
            ReservationTime = reservation.ReservationTime.ToString(),
            PeopleCount = reservation.PeopleCount,
            Status = reservation.Status.ToString()
        };
    }

    public async Task<bool> AddReservationAsync(ReservationDto reservationDto)
    {
        try
        {
            var user = await _userRepository.GetUserByEmailAsync(reservationDto.EmployeeEmail);
            if (user == null)
                throw new Exception("User not found.");
            var restaurant = await _restaurantrepo.GetByIdAsync(reservationDto.RestaurantId);
            if (restaurant == null) throw new Exception("Restaurant not found.");
            if (!DateTime.TryParse(reservationDto.ReservationDate, out DateTime reservationDate))
                throw new Exception("Invalid date format.");

            if (!TimeSpan.TryParse(reservationDto.ReservationTime, out TimeSpan reservationTime))
                throw new Exception("Invalid time format.");

            bool isTableReserved = await _reservationRepository.TableIsReservedAsync(reservationDto.TableId, reservationDate, reservationTime);
            if (isTableReserved)
                throw new Exception("Table is already reserved at this time.");

            var orderItems = new List<OrderItem>();
            foreach (var item in reservationDto.OrderItems)
            {
                var menuItem = await _menuItemRepository.GetMenuItemByNameAsync(item.Name);
                if (menuItem == null)
                    throw new Exception($"Menu item '{item.Name}' not found in the database.");

                orderItems.Add(new OrderItem { MenuItemId = menuItem.Id });
            }
            var table = await _tableRepository.GetTableByIdAsync(reservationDto.TableId);
            if (table == null) throw new Exception("Table not found.");
            if (reservationDto.PeopleCount < 1)
            {
                throw new Exception("People count must be at least 1.");
            }
            if (orderItems == null || !orderItems.Any())
                throw new Exception("Order items cannot be empty.");
            var reservation = new Reservation
            {
                RestaurantId = reservationDto.RestaurantId,
                EmployeeEmail = user.EmployeeEmail,
                UserId = user.EmployeeId,
                TableId = reservationDto.TableId,
                ReservationDate = reservationDate,
                ReservationTime = reservationTime,
                PeopleCount = reservationDto.PeopleCount,
                OrderItems = orderItems,
                Status = Reservation_Status.Completed // ✅ تحديد حالة الحجز عند الإنشاء

            };

            await _reservationRepository.AddReservationAsync(reservation);

            await Task.Run(() => _emailService.SendEmailAsync(user.EmployeeEmail, "Reservation Confirmation", $@"
                Dear {user.EmployeeName},  
                Your reservation at : {restaurant.Name} restaurant is confirmed.  
                📅 Date: {reservationDto.ReservationDate}  
                ⏰ Time: {reservationDto.ReservationTime}  
                👥 People: {reservationDto.PeopleCount}  
                🍜 Food Items:  
                {string.Join("\n    - ", reservationDto.OrderItems.Select(item => item.Name))}  

                Thank you for choosing our service!
              "));


            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating reservation");

            if (ex.InnerException != null)
            {
                _logger.LogError(ex.InnerException, "Inner exception details");
                throw new Exception($"Reservation error: {ex.InnerException.Message}");
            }

            throw new Exception($"Reservation error: {ex.Message}");
        }

    }

    public async Task<bool> UpdateReservationAsync(int id, UpdateReservationDto updatedReservationDto)
    {
        var existingReservation = await _reservationRepository.GetReservationByIdAsync(id);
        if (existingReservation == null) return false;

        if (!DateTime.TryParse(updatedReservationDto.ReservationDate, out DateTime reservationDate))
            throw new Exception("Invalid date format.");

        if (!TimeSpan.TryParse(updatedReservationDto.ReservationTime, out TimeSpan reservationTime))
            throw new Exception("Invalid time format.");

        existingReservation.ReservationDate = reservationDate;
        existingReservation.ReservationTime = reservationTime;
        existingReservation.PeopleCount = updatedReservationDto.PeopleCount;

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