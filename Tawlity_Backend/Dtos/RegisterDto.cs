using System.ComponentModel.DataAnnotations;
using Tawlity.Core.Enums;
using Tawlity_Backend.Data.Enums;

namespace Tawlity_Backend.Dtos
{
    public class RegisterDto
    {
        [StringLength(30, MinimumLength = 3, ErrorMessage = "User name should between 3 to 30 chars")]
        public string EmployeeName { get; set; } = string.Empty;
        public Employee_Gender EmployeeGender { get; set; }

        [Phone(ErrorMessage = "Your phone number less than 11")]
        public string EmployeePhone { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "The email not valid")]
        public string EmployeeEmail { get; set; } = string.Empty;
        public Employee_City EmployeeCity { get; set; }
        [RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}",
    ErrorMessage = "Password must be at least 8 characters long, including uppercase, lowercase, a number, and a special character.")]
        public string EmployeePassword { get; set; } = string.Empty;

        [Compare("EmployeePassword", ErrorMessage = "User comfirm password not mathing with password")]
        public string EmployeeConfirmPassword { get; set; } = string.Empty;
        [CreditCard(ErrorMessage = "Invalid credit card number")]
        public string EmployeeCreditCard { get; set; } = string.Empty;
        public Employee_Role Employee_Role { get; set; }
    }

}
