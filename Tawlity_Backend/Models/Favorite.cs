using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Models
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }

        // Only one of these can be non-null
        public int? RestaurantId { get; set; }
        public virtual Restaurant ?Restaurant { get; set; }

        public int? MenuItemId { get; set; }
        public virtual MenuItem ?MenuItem { get; set; }

        [Required]
        public DateTime AddedOn { get; set; } = DateTime.UtcNow;
    }
}
