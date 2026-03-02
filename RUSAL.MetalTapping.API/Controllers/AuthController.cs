using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Contracts;
using RUSAL.MetalTapping.BLL.Services;

namespace RUSAL.MetalTapping.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequest model)
        {
            await _userService.Register(model);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserRequest model)
        {
            await _userService.Login(model);
            return Ok();
        }
    }
}
