using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(1000)]
        public string ?Comment { get; set; }

        // Relationships
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User ?User { get; set; }

        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public virtual Restaurant ?Restaurant { get; set; }
    }
}
