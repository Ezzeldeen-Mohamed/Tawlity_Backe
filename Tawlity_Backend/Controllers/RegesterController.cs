using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tawlity_Backend.Dtos;
using Tawlity_Backend.Services.IService;

namespace Tawlity_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegesterController : ControllerBase
    {
        private readonly Login_IService _loginService;

        public RegesterController(Login_IService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _loginService.LoginAsync(loginDto);

                return Ok(new
                {
                    Message = "Login successful",
                    Token = result
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid registration data.");
            }

            try
            {
                var result = await _loginService.RegesterAsync(registerDto);

                return Ok(new
                {
                    Message = "Registration successful",
                    Token = result
                });
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var token = await _loginService.ForgotPasswordAsync(dto);
            if (token == "User not found.")
                return NotFound(token);

            // Simulating token return for simplicity
            return Ok(new { ResetToken = token });
        }

        [HttpPost("reset-password/{token}")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string token, ResetPasswordDto dto)
        {
            var result = await _loginService.ResetPasswordAsync(token, dto);
            if (result == "Invalid or expired token." || result == "Passwords do not match.")
                return BadRequest(result);

            return Ok(result);
        }

    }
}
