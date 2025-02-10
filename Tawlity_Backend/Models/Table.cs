using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Models
{
    public class Table
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 20)]
        public int Capacity { get; set; }

        public string ?ImageUrl { get; set; } // Image of the table

        // Relationships
        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        public virtual Branch ?Branch { get; set; }

        public virtual Image? Image { get; set; }


        public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
    }
}
