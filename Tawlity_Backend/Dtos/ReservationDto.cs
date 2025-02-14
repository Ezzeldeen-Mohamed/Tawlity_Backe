using Tawlity_Backend.Data.Enums;

namespace Tawlity_Backend.Dtos
{
    public class ReservationDto
    {
        public int RestaurantId { get; set; }
        public int TableId { get; set; }
        public DateOnly ReservationDate { get; set; }
        public TimeOnly ReservationTime { get; set; }
        public int PeopleCount { get; set; }
       public Reservation_Status Status { get; set; } // Enum for status
        public List<MenuItemD> OrderItems { get; set; } = new List<MenuItemD>();
        //list of menu items  for every items have list of items 
    }
    public class UpdateReservationDto
    {
        public DateOnly ReservationDate { get; set; }
        public TimeOnly ReservationTime { get; set; }
        public int PeopleCount { get; set; }
        public Reservation_Status Status { get; set; } // Enum for status
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>(); // ✅ Used Here

    }
    public class ReservationResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int TableId { get; set; }
        public string RestaurantName { get; set; }
        public DateOnly ReservationDate { get; set; }
        public TimeOnly ReservationTime { get; set; }
        public int PeopleCount { get; set; }
        public string Status { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>(); // ✅ Used Here
    }


}
