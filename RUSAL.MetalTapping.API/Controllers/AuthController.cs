using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.UseCases;

namespace RUSAL.MetalTapping.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly RegisterUserUseCase _registerUserUseCase;
        private readonly LoginUserUseCase _loginUserUseCase;

        public AuthController(
            RegisterUserUseCase registerUserUseCase,
            LoginUserUseCase loginUserUseCase)
        {
            _registerUserUseCase = registerUserUseCase;
            _loginUserUseCase = loginUserUseCase;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequest model)
        {
            await _registerUserUseCase.ExecuteAsync(model);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserRequest model)
        {
            var token = await _loginUserUseCase.ExecuteAsync(model);
            return Ok(token);
        }
    }
}