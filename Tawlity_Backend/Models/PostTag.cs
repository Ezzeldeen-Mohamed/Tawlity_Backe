using System.ComponentModel.DataAnnotations.Schema;

namespace Tawlity_Backend.Models
{
    public class PostTag
    {
        [ForeignKey("CommunityPost")]
        public int PostId { get; set; }
        public virtual CommunityPost ?Post { get; set; }

        [ForeignKey("Tag")]
        public int TagId { get; set; }
        public virtual Tag ?Tag { get; set; }
    }
}
