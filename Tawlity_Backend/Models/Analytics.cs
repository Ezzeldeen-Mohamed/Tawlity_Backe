using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Models
{
    public class Analytics
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int TotalReservations { get; set; }
        public decimal TotalRevenue { get; set; }
        public int ActiveUsers { get; set; }

        // Relationships
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }=new Restaurant();
    }
}
