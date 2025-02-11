using Tawlity_Backend.Dtos;

namespace Tawlity_Backend.Services.IService
{
    public interface Login_IService
    {
        //With Dtos
        Task<string> LoginAsync(LoginDto loginDto);
        Task<string> RegesterAsync(RegisterDto registerDto);
        Task<string> ForgotPasswordAsync(ForgotPasswordDto dto);
        Task<string?> ResetPasswordAsync(string token, ResetPasswordDto dto);
    }
}
