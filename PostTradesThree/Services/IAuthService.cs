using PostTradesThree.Dtos;

namespace PostTradesThree.Services
{
    public interface IAuthService
    {
        Task<AuthServiceResponseDto> SeedRolesAsync();
        Task<AuthServiceResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthServiceResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthServiceResponseDto> MakeAdminAsync(UpdateRoleDto updateRoleDto);
        Task<AuthServiceResponseDto> MakeOwnerAsync(UpdateRoleDto updateRoleDto);
        Task<AuthServiceResponseDto> LogoutAsync();
    }
}
