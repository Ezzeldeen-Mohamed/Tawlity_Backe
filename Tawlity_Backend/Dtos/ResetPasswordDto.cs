using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Dtos
{
    public class ResetPasswordDto
    {
        [Required]
        public string Token { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[\d])(?=.*[!@#$%^&*]).{8,}$",
         ErrorMessage = "Password must be at least 8 characters long and include uppercase, lowercase, digit, and special character.")]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
    public class ResetPasswordResponseDto
    {
        public string Message { get; set; } = string.Empty;
    }
}
