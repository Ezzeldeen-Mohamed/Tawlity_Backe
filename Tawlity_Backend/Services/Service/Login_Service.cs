using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;
using Tawlity_Backend.Services.Interface;
using Tawlity_Backend.Services.IService;
using BCrypt.Net;
using Org.BouncyCastle.Tls;
using Tawlity.Core.Enums;
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
                Employee_Role=Employee_Role.Customer
            };


            // Save the employee to the database
            await _repository.AddEmployeeAsync(employee);

            // Send confirmation email
            await _emailService.SendEmailAsync(registerDto.EmployeeEmail, "Welcome to Tawlity 🎉", $@"
                <h2>Hello {registerDto.EmployeeName},</h2>
                <p>Thank you for registering on <b>Tawlity</b>! 🎉</p>
                <p>You can now start making restaurant reservations and enjoy our services.</p>
                <p>Best regards,<br/>The Tawlity Team</p>
                  ");

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
                new Claim(ClaimTypes.NameIdentifier, employee.EmployeeId.ToString()), // ✅ User ID
                new Claim(ClaimTypes.Name, employee.EmployeeName),
                new Claim(ClaimTypes.Role, employee.Employee_Role.ToString()), // ✅ Use ClaimTypes.Role for proper authorization
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:ExpiresInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string?> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await _repository.GetEmployeeByEmailAsync(dto.Email);

            if (user == null)
                return "Email not registered.";
            else
            {
                var token = Guid.NewGuid().ToString();
                user.ResetToken = token;
                user.ResetTokenExpiry = DateTime.UtcNow.AddHours(1);
                await _repository.SaveChangesAsync();

                var subject = "Reset Your Password";
                //var resetLink = $"https://localhost:7039/api/Regester/reset-password?token={token}";
                var resetLink = $"This is your token:( {token} )";
                //var body = $"<p>Click <a href='{resetLink}'>here</a> to reset your password.</p>";
                var body = $"<p> {resetLink} to reset your password.</p>";

                await _emailService.SendEmailAsync(dto.Email, subject, body);

                return "Password reset link has been sent to your email.";
            }
        }

        public async Task<string?> ResetPasswordAsync(string token, ResetPasswordDto dto)
        {
            var user = await _repository.GetUserByResetTokenAsync(token);
            if (user == null)
                return "Invalid or expired token.";

            if (dto.NewPassword != dto.ConfirmPassword)
                return "Passwords do not match.";

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            user.ResetToken = null;
            user.ResetTokenExpiry = null;
            user.EmployeeConfirmPassword=HashPassword(dto.ConfirmPassword);
            user.EmployeePassword=HashPassword(dto.NewPassword);
            await _repository.SaveChangesAsync();

            return "Password changed successfully.";
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