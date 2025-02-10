using System.ComponentModel.DataAnnotations;

namespace Tawlity_Backend.Dtos
{
    public class LoginDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "The email is not valid")]
        public string EmployeeEmail { get; set; } = string.Empty;

        [Required]
        public string EmployeePassword { get; set; } = string.Empty;
    }
    public class Login_Message
    {
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public bool Status { get; set; }
        public LoginDto? EmployeeDto { get; set; } = null;
    }

}
