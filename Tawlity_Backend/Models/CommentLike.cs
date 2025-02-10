using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Models
{
    public class CommentLike
    {
        [Key]
        public int Id { get; set; }

        // Relationships
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User ?User { get; set; }

        [ForeignKey("Comment")]
        public int CommentId { get; set; }
        public virtual Comment? Comment { get; set; }
    }
}
