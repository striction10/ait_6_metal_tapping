using RUSAL.MetalTapping.BLL.Application.Services;

namespace RUSAL.MetalTapping.BLL.Application.UseCases;

/// <summary>
/// Оркестратор сервисов по удалению пользователя.
/// </summary>
/// <param name="userService">Сервис удаления пользвателя.</param>
public class DeleteUserUseCase(UserService userService)
{
    /// <summary>
    /// Удаление пользователя по идентификатору.
    /// </summary>
    /// <param name="email">Email пользователя.</param>
    public async Task ExecuteAsync(string email)
    {
        var user = await userService.GetByEmailAsync(email);

        await userService.DeleteAsync(user);
    }
}
