using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        // Relationships
        [ForeignKey("Reservation")]
        public int ReservationId { get; set; }
        public virtual Reservation ?Reservation { get; set; }

        [ForeignKey("MenuItem")]
        public int MenuItemId { get; set; }
        public virtual MenuItem ?MenuItem { get; set; }
    }
}
