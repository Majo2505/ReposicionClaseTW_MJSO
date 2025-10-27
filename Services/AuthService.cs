using EC4clase1.Models;
using EC4clase1.Models.dtos;
using EC4clase1.Models.Responses;
using EC4clase1.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace EC4clase1.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        public AuthService (IUserRepository user, IConfiguration configuration)
        {
            _userRepository = user;
            _configuration = configuration;
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
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = registerDto.Role
            };
            await _userRepository.AddUser(user);
            var response = new UserResponse
            {
                RequestId = user.Id.ToString(),
                Email = registerDto.Email
            };
            return response;
        }
        public async Task<(bool ok, string? token)> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetUserByEmail(dto.Email);
            if (user == null) return (false, null);
            var ok = BCrypt.Net.BCrypt.Verify(dto.Password,user.PasswordHash);
            if (!ok) return (false, null);
            var token = GenerateJwtToken(user);
            return (ok, token);
        }

        public async Task<bool> VerifyCredentials(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByEmail(loginDto.Email);
            return BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);

        }
        private string GenerateJwtToken(User user)
        {
            var jwtSection = _configuration.GetSection("Jwt");
            var key = jwtSection["Key"];
            var issuer = jwtSection["Issuer"];
            var audience = jwtSection["Audience"];
            var expiresMinutes = int.Parse(jwtSection["ExpiresMinutes"]?? "60");
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var creds = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
                signingCredentials: creds

            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
