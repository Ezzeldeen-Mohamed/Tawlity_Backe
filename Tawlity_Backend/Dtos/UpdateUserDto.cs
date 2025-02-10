using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Dtos
{
    public class UpdateUserDto
    {

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
