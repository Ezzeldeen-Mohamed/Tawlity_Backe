using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tawlity_Backend.Data.Enums;

namespace Tawlity_Backend.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateOnly ReservationDate{ get; set; }   //make here is date time and add new parameter data only 
        public TimeOnly ReservationTime { get; set; }   
     
        [Required]
        [Range(1, 2000)]
        public int PeopleCount { get; set; }

        [Required]
        public Reservation_Status? Status { get; set; } 

        // Relationships
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User ?User { get; set; }

        [ForeignKey("Table")]
        public int TableId { get; set; }
        public virtual Table ?Table { get; set; } 

        [ForeignKey("Branch")]
        public int BranchId { get; set; }  // New Relationship
        public virtual Branch? Branch { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }=new HashSet<OrderItem>();
    }
}
