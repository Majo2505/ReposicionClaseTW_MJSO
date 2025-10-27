using EC4clase1.Models.dtos;
using EC4clase1.Models.Responses;

namespace EC4clase1.Services
{
    public interface IAuthService
    {
        //Task<string> LoginAsync(LoginDto dto);
        Task<UserResponse> LoginAsync1(LoginDto loginDto);
        Task<UserResponse> RegisterAsync(RegisterDto registerDto);
        Task<bool> VerifyCredentials(LoginDto loginDto);
        
    }
}
