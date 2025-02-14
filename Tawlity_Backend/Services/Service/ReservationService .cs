using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;
using Tawlity_Backend.Services.IService;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public ReservationService(IReservationRepository reservationRepository, IUserRepository userRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReservationResponseDto>> GetAllReservationsAsync()
    {
        var reservations = await _reservationRepository.GetAllReservationsAsync();
        return _mapper.Map<IEnumerable<ReservationResponseDto>>(reservations);
    }

    public async Task<IEnumerable<ReservationResponseDto>> GetReservationsByUserIdAsync(int userId)
    {
        var reservations = await _reservationRepository.GetReservationsByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<ReservationResponseDto>>(reservations);
    }

    public async Task<ReservationResponseDto?> GetReservationByIdAsync(int id)
    {
        var reservation = await _reservationRepository.GetReservationByIdAsync(id);
        return _mapper.Map<ReservationResponseDto>(reservation);
    }

    void IReservationService.AddReservationAsync(int userId, ReservationDto reservationDto)
    {
            var reservation = new Reservation
            {
                RestaurantId = reservationDto.RestaurantId,
                UserId = userId, // Assign the logged-in user ID
                TableId = reservationDto.TableId,
                ReservationDate = reservationDto.ReservationDate,
                ReservationTime = reservationDto.ReservationTime,
                PeopleCount = reservationDto.PeopleCount,
                Status = reservationDto.Status,
                OrderItems = reservationDto.OrderItems.Select(x => new OrderItem
                {
                    MenuItem = new MenuItem
                    {
                        Price = x.Price,
                        Name = x.Name,
                    }
                }).ToList()
            };
        if (reservation == null)
        {
            throw new Exception("Enter data");
        }
        _reservationRepository.AddReservationAsync(reservation);
    }
    public async Task<bool> UpdateReservationAsync(int id, UpdateReservationDto updatedReservationDto)
    {
        var existingReservation = await _reservationRepository.GetReservationByIdAsync(id);
        if (existingReservation == null) return false;

        _mapper.Map(updatedReservationDto, existingReservation);
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
