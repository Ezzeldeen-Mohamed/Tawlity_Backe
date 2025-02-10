using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Models
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ?Type { get; set; } // "Monthly", "Annual"

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        // Relationships
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User ?User { get; set; }
    }
}
