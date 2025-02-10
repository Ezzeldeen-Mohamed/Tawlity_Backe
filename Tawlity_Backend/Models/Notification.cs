using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string? Message { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        public bool IsRead { get; set; }

        // Relationships
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
