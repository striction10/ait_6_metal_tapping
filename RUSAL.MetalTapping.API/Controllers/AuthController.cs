using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.UseCases;

namespace RUSAL.MetalTapping.API.Controllers;

/// <summary>
/// Контроллер аутентификации.
/// </summary>
/// <param name="registerUserUseCase"> Оркестратор для работы сервисов с регистрацией пользователя. </param>
/// <param name="loginUserUseCase"> Оркестратор для работы сервисов с авторизацией пользователя. </param>
[ApiController]
[Route("api/[controller]")]
public class AuthController(
    RegisterUserUseCase registerUserUseCase,
    LoginUserUseCase loginUserUseCase) : ControllerBase
{
    /// <summary>
    /// Регистрация пользователя в системе.
    /// </summary>
    /// <param name="model"> Параметры регистрации. </param>
    [HttpPost("register")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Register(RegisterUserRequest model)
    {
        await registerUserUseCase.ExecuteAsync(model);

        return Ok();
    }

    /// <summary>
    /// Регистрация пользователя в системе.
    /// </summary>
    /// <param name="model"> Параметры авторизации. </param>
    /// <returns> Токен. </returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest, "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/json")]
    public async Task<ActionResult<LoginResponse>> Login(LoginUserRequest model)
    {
        var token = await loginUserUseCase.ExecuteAsync(model);

        return Ok(new LoginResponse { Token = token });
    }
}
