using System.ComponentModel.DataAnnotations;
using Tawlity_Backend.Data.Enums;

namespace Tawlity_Backend.Dtos
{
    public class ReservationDto
    {
        [EmailAddress]
        public string EmployeeEmail { get; set; }
        public int RestaurantId { get; set; }
        public int TableId { get; set; }
        public string ReservationDate { get; set; } // استقبال كـ string ثم تحويله لـ DateTime
        public string ReservationTime { get; set; } // استقبال كـ string ثم تحويله لـ TimeSpan
        public int PeopleCount { get; set; }
        public List<MenuItemD> OrderItems { get; set; } = new List<MenuItemD>();
    }

    public class UpdateReservationDto
    {
        public string ReservationDate { get; set; }
        public string ReservationTime { get; set; }
        public int PeopleCount { get; set; }
        public string Status { get; set; } // Enum for status
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>(); // ✅ Used Here

    }
    public class ReservationResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int TableId { get; set; }
        public string RestaurantName { get; set; }
        public int RestaurantId { get; set; }
        public string ReservationDate { get; set; }
        public string ReservationTime { get; set; }
        public int PeopleCount { get; set; }
        public string Status { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>(); // ✅ Used Here
    }

    public class ReservationForProfileDto
    {
        public int UserId { get; set; }
        public int ReservationId { get; set; }
        public DateTime ReservationDate { get; set; }
        public string RestaurantName { get; set; }
    }



}
