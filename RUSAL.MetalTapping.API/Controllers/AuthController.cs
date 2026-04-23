using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.UseCases;
namespace RUSAL.MetalTapping.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    RegisterUserUseCase registerUserUseCase,
    LoginUserUseCase loginUserUseCase) : ControllerBase
{
    private readonly RegisterUserUseCase _registerUserUseCase = registerUserUseCase;
    private readonly LoginUserUseCase _loginUserUseCase = loginUserUseCase;

    /// <summary>
    /// Регистрация пользователя в системе
    /// </summary>
    /// <param name="model"> Параметры регистрации </param>
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest model)
    {
        await _registerUserUseCase.ExecuteAsync(model);

        return Ok();
    }

    /// <summary>
    /// Регистрация пользователя в системе
    /// </summary>
    /// <param name="model"> Параметры регистрации </param>
    /// <returns> Токен </returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<string>> Login(LoginUserRequest model)
    {
        var token = await _loginUserUseCase.ExecuteAsync(model);

        return Ok(token);
    }
}