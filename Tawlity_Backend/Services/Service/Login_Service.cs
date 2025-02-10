using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;
using Tawlity_Backend.Services.Interface;
using Tawlity_Backend.Services.IService;
using Tawlity_Backend.SomeThingsWeWillUseInTheFuther;

namespace Tawlity_Backend.Services.Service
{
    public class Login_Service:Login_IService
    {
        //With Dtos
        private readonly Login_IRepo _repository;
        private readonly IConfiguration _config;
        private readonly EmailService _emailService;
        public Login_Service(Login_IRepo repository, IConfiguration config, EmailService emailService)
        {
            _repository = repository;
            _config = config;
            _emailService = emailService;
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            // Validate credentials
            var employee = await _repository.GetEmployeeByEmailAsync(loginDto.EmployeeEmail);
            if (employee == null ||employee.EmployeePassword != HashPassword(loginDto.EmployeePassword))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
            return GenerateJwtToken(employee);
        }

        public async Task<string> RegesterAsync(RegisterDto registerDto)
        {
            // Validate input
            if (registerDto.EmployeePassword != registerDto.EmployeeConfirmPassword)
            {
                throw new ArgumentException("Passwords do not match."); 
            }

            // Check if the email already exists
            var existingEmployee = await _repository.GetEmployeeByEmailAsync(registerDto.EmployeeEmail);
            if (existingEmployee != null)
            {
                throw new InvalidOperationException("An account with this email already exists.");
            }

            // Create a new employee entity
            var employee = new User
            {
                EmployeeName = registerDto.EmployeeName,
                EmployeePassword = HashPassword(registerDto.EmployeePassword), // Use a hashing method for secure passwords
                EmployeeConfirmPassword = HashPassword(registerDto.EmployeeConfirmPassword),
                EmployeeEmail = registerDto.EmployeeEmail,
                EmployeeCity = registerDto.EmployeeCity,
                EmployeeCreditCard = registerDto.EmployeeCreditCard,
                EmployeeGender = registerDto.EmployeeGender,
                EmployeePhone = registerDto.EmployeePhone,
                Employee_Role = registerDto.Employee_Role
            };

            // Save the employee to the database
            await _repository.AddEmployeeAsync(employee);

            // Generate and return a JWT token for the registered user
            return GenerateJwtToken(employee);
        }

        // Helper method to hash passwords securely
        private string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private string GenerateJwtToken(User employee)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, employee.EmployeeName),
                new Claim(ClaimTypes.NameIdentifier, employee.EmployeeId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_config["Jwt:ExpiresInMinutes"]!)),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ForgotPasswordResponseDto> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var employee = await _repository.GetEmployeeByEmailAsync(dto.Email);
            if (employee == null)
            {
                // Always return a generic message for security
                return new ForgotPasswordResponseDto
                {
                    Message = "If an account with that email exists, a password reset link has been sent."
                };
            }

            // Generate reset token and set expiry
            var token = Guid.NewGuid().ToString();
            employee.ResetToken = token;
            employee.ResetTokenExpiry = DateTime.UtcNow.AddHours(1);
            await _repository.UpdateEmployeeAsync(employee);

            // In production, integrate with an email service to send the reset link.
            return new ForgotPasswordResponseDto
            {
                Message = $"Password reset link has been sent to {employee.EmployeeEmail}",
                ResetToken = token // For demo/testing purposes only.
            };
        }
        public async Task<ResetPasswordResponseDto> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var employee = await _repository.GetEmployeeByResetTokenAsync(dto.Token);
            if (employee == null || employee.ResetTokenExpiry == null || employee.ResetTokenExpiry < DateTime.UtcNow)
            {
                throw new Exception("Invalid or expired token.");
            }
            // Update the user's password with a hashed version.
            // Clear the reset token fields.
            employee.ResetToken = null;
            employee.ResetTokenExpiry = null;

            employee.EmployeePassword= HashPassword(dto.NewPassword);
            employee.EmployeeConfirmPassword = HashPassword(dto.ConfirmPassword);

            await _repository.UpdateEmployeeAsync(employee);
            return new ResetPasswordResponseDto
            {
                Message = "Password has been reset successfully."
            };
        }


    }
}
/*
        public async Task<ForgotPasswordResponseDto> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var employee = await _repository.GetEmployeeByEmailAsync(dto.Email);
            if (employee == null)
            {
                // Always return a generic message for security
                return new ForgotPasswordResponseDto
                {
                    Message = "If an account with that email exists, a password reset link has been sent."
                };
            }

            // Generate reset token and set expiry
            var token = Guid.NewGuid().ToString();
            employee.ResetToken = token;
            employee.ResetTokenExpiry = DateTime.UtcNow.AddHours(1);
            await _repository.UpdateEmployeeAsync(employee);

            // In production, integrate with an email service to send the reset link.
            return new ForgotPasswordResponseDto
            {
                Message = "Password reset link has been sent to your email.",
                ResetToken = token // For demo/testing purposes only.
            };
        }



 public async Task<ForgotPasswordResponseDto> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await _repository.GetEmployeeByEmailAsync(dto.Email);
            if (user == null)
            {
                // Always return a generic message for security
                return new ForgotPasswordResponseDto
                {
                    Message = "If an account with that email exists, a password reset link has been sent."
                };
            }

            // Generate reset token
            var token = Guid.NewGuid().ToString();
            user.ResetToken = token;
            user.ResetTokenExpiry = DateTime.UtcNow.AddHours(1);
            await _repository.UpdateEmployeeAsync(user);

            // Construct reset link
            string resetLink = $"https://your-frontend.com/reset-password?token={token}";
            string emailBody = $"<p>Click <a href='{resetLink}'>here</a> to reset your password.</p>";

            // Send email
            await _emailService.SendEmailAsync(user.EmployeeEmail, "Password Reset Request", emailBody);

            return new ForgotPasswordResponseDto
            {
                Message = "Password reset link has been sent to your email.",
            };
        }
 */