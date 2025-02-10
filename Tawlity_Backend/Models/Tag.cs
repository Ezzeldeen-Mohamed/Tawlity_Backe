using System.ComponentModel.DataAnnotations;

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
        public virtual ICollection<PostTag> PostTags { get; set; } = new HashSet<PostTag>();
    }
}
