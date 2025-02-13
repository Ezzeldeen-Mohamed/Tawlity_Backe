using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Dtos
{
    public class CreatePostDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(2000)]
        public string Content { get; set; }

        [Required]
        public int UserId { get; set; }
    }

    public class PostResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateCommentDto
    {
        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int PostId { get; set; }
    }

    public class CommentResponseDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int PostId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
