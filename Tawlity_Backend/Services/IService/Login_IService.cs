using Tawlity_Backend.Dtos;

namespace Tawlity_Backend.Services.IService
{
    public interface Login_IService
    {
        //With Dtos
        Task<string> LoginAsync(LoginDto loginDto);
        Task<string> RegesterAsync(RegisterDto registerDto);
        Task<ForgotPasswordResponseDto> ForgotPasswordAsync(ForgotPasswordDto dto);
        Task<ResetPasswordResponseDto> ResetPasswordAsync(ResetPasswordDto dto);
    }
}
