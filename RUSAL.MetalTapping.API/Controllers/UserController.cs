using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.UseCases;
using RUSAL.MetalTapping.BLL.Application.ViewModels;

namespace RUSAL.MetalTapping.API.Controllers;

/// <summary>
/// Контроллер пользователей.
/// </summary>
/// <param name="getAllUsersUseCase">Оркестратор для работы сервисов с пользователями.</param>
[ApiController]
[Route("api/[controller]")]
public class UserController(
    GetAllUsersUseCase getAllUsersUseCase,
    DeleteUserUseCase deleteUserUseCase) : ControllerBase
{
    /// <summary>
    /// Получение всех пользователей.
    /// </summary>
    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<UserViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAllUsers()
    {
        var response = await getAllUsersUseCase.ExecuteAsync();

        return Ok(response);
    }

    /// <summary>
    /// Удаление пользователя по адресу почты.
    /// </summary>
    /// <param name="email">Адрес почты пользователя.</param>
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<UserViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteUser(
        [FromQuery] string email)
    {
        await deleteUserUseCase.ExecuteAsync(email);

        return Ok();
    }
}
