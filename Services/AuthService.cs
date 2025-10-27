using EC4clase1.Models;
using EC4clase1.Models.dtos;
using EC4clase1.Models.Responses;
using EC4clase1.Repositories;

namespace EC4clase1.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        public AuthService (IUserRepository user)
        {
            _userRepository = user;
        }

        public async Task<UserResponse> LoginAsync1(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByEmail(loginDto.Email);
            var passwirdIsEqual = BCrypt.Net.BCrypt.Verify(loginDto.Password,user.PasswordHash); // el verify de Bcrypt, necesita dos parametros, pq va a comparar
            if(!passwirdIsEqual)
            {
                throw new Exception("invalid Password");
            }
            return new UserResponse
            {
                Email = user.Email
            };
        }

        public async Task<UserResponse> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };
            await _userRepository.AddUser(user);
            var response = new UserResponse
            {
                Email = registerDto.Email
            };
            return response;
        }
        //public async Task<string> LoginAsync(LoginDto dto)
        //{
        //    await VerifyCredentials(dto);
        //    var token = await GenerateJwtToken();
        //}

        public async Task<bool> VerifyCredentials(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByEmail(loginDto.Email);
            return BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);

        }
    }
}
