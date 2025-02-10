using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tawlity_Backend.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime ReservationDateTime { get; set; }

        [Required]
        [Range(1, 20)]
        public int PeopleCount { get; set; }

        [Required]
        public string ?Status { get; set; } // "Pending", "Confirmed", "Cancelled"

        // Relationships
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User ?User { get; set; }

        [ForeignKey("Table")]
        public int TableId { get; set; }
        public virtual Table ?Table { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }=new HashSet<OrderItem>();
    }
}
