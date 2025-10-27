using EC4clase1.Models.dtos;
using EC4clase1.Services;
using Microsoft.AspNetCore.Mvc;

namespace EC4clase1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            return CreatedAtAction(nameof(Register), result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var isvalid = await _authService.VerifyCredentials(dto);
            if(!isvalid)
            {
                return Unauthorized();
            }
            return Ok(dto);
            //try
            //{
            //    var result = await _authService.LoginAsync(dto);
            //    //return CreatedAtAction(nameof(Login), result); // asi estaba en clases //mgr no debería ser solo Ok(result) pq solo estamos comparando y no creando un nuevo objeto
            //    return Ok(result);
            //}
            //catch (Exception ex)
            //{
            //    return Unauthorized(new { message = ex.Message });
            //}
        }
        //tarea
        //subir capturas de postman de login
        //completar el appsetting
        //agregar el docker compose
        //inyeccion de dependencias de program
        //probar el postman
    }
}
