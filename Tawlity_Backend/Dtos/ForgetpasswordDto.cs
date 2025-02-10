using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Dtos
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
    public class ForgotPasswordResponseDto
    {
        public string Message { get; set; } = string.Empty;
        // For demo purposes only. In production, do not return the token.
        public string? ResetToken { get; set; }
    }
}
