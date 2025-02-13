using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Models
{
    public class CommunityPost
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string ?Title { get; set; }

        [Required]
        [StringLength(2000)]
        public string ?Content { get; set; }

        public int Likes { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relationships
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User ?User { get; set; }

        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public virtual Restaurant ?Restaurant { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
        
    }
}
