using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tawlity.Core.Enums;
using Tawlity_Backend.Data.Enums;

namespace Tawlity_Backend.Models
{
    public class User
    {
        [Key]
        public int EmployeeId { get; set; }

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
        public string PasswordHash { get; set; } = string.Empty;
        public string? ResetToken { get; set; }= string.Empty;
        public DateTime? ResetTokenExpiry { get; set; }= new DateTime(2025, 3, 10, 19, 00, 00);

        // Relationships
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }  // New Relationship
        public virtual Restaurant? Restaurant { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
        public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();

    }
}


/*
    do this end points for me do dtos and service and repo and controller don't use mapper or mappester and do relation between the nother tables
 */