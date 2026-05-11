using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.BLL.Application.UseCases;

/// <summary>
/// Оркестратор авторизации пользователя в системе.
/// </summary>
/// <param name="userService">Сервис для работы с пользователями.</param>
/// <param name="passwordHasher">Сервис хеширования и проверки паролей.</param>
/// <param name="jwtProvider">Провайдер генерации JWT-токенов.</param>
/// <param name="roleRepository">Репозиторий ролей.</param>
public class LoginUserUseCase(
    UserService userService,
    IPasswordHasher passwordHasher,
    IJwtProvider jwtProvider,
    IRoleRepository roleRepository)
{
    /// <summary>
    /// Авторизация пользователя.
    /// </summary>
    /// <param name="model"> Данные для авторизации. </param>
    /// <returns> Токен авторизованного пользователя. </returns>
    /// <exception cref="NotFoundException"> Пользователя нет в системе. </exception>
    /// <exception cref="AuthentificationException"> Неверный пароль. </exception>
    public async Task<string> ExecuteAsync(LoginUserRequest model)
    {
        var user = await userService.GetByEmailAsync(model.email);
        if (user == null)
        {
            throw new NotFoundException($"User with email {model.email} was not found");
        }

        var valid = passwordHasher.Verify(model.password, user.Password);
        if (!valid)
        {
            throw new AuthentificationException("Invalid login attempt");
        }

        var role = await roleRepository.GetUserRoleAsync(user.Id);

        return jwtProvider.GenerateJwtToken(user, role.Name);
    }
}
