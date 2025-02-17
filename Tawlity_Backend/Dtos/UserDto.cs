using System.ComponentModel.DataAnnotations;
using Tawlity.Core.Enums;
using Tawlity_Backend.Data.Enums;

namespace Tawlity_Backend.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }

    public class UsersDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public Employee_Gender EmployeeGender { get; set; }
        public string EmployeePhone { get; set; } = string.Empty;
        public string EmployeeEmail { get; set; } = string.Empty;
        public Employee_City EmployeeCity { get; set; }
        public Employee_Role Employee_Role { get; set; }
        public string EmployeeCreditCard { get; set; } = string.Empty;
    }

    public class CreateUserDto
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string EmployeeName { get; set; } = string.Empty;

        [Required]
        public Employee_Gender EmployeeGender { get; set; }

        [Required]
        [Phone]
        public string EmployeePhone { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string EmployeeEmail { get; set; } = string.Empty;

        [Required]
        public Employee_City EmployeeCity { get; set; }

        [Required]
        public Employee_Role Employee_Role { get; set; }

        [Required]
        [CreditCard]
        public string EmployeeCreditCard { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string EmployeePassword { get; set; } = string.Empty;

        [Required]
        [Compare("EmployeePassword")]
        public string EmployeeConfirmPassword { get; set; } = string.Empty;
    }

    public class UpdateUserDto
    {
        public string? EmployeeName { get; set; }
        public string? EmployeePhone { get; set; }
        public Employee_City? EmployeeCity { get; set; }
    } 
    public class DeleteUserDto
    {
        public string? EmployeeEmail { get; set; }
    }

}
