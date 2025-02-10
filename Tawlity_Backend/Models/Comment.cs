using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        // Relationships
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }

        public int PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        public virtual CommunityPost ?Post { get; set; }

        public virtual ICollection<CommentLike> Likes { get; set; } = new HashSet<CommentLike>();
    }
}
