using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tawlity_Backend.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ?Name { get; set; }

        // Relationships
        public virtual ICollection<CommunityPost> CommunityPosts { get; set; } = new HashSet<CommunityPost>();

    }
}
