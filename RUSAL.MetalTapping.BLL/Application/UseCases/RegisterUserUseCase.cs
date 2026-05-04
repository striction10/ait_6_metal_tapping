using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.BLL.Application.UseCases;

/// <summary>
/// Оркестратор регистрации нового пользователя в системе.
/// </summary>
/// <param name="userService">Сервис для работы с пользователями.</param>
/// <param name="roleService">Сервис для работы с ролями.</param>
/// <param name="passwordHasher">Сервис хеширования паролей.</param>
/// <param name="userRoleMembersService">Сервис для работы со связями пользователей и ролей.</param>
public class RegisterUserUseCase(
    UserService userService,
    RoleService roleService,
    IPasswordHasher passwordHasher,
    UserRoleMembersService userRoleMembersService)
{
    /// <summary>
    /// Регистрация пользователя в системе.
    /// </summary>
    /// <param name="model"> Данные для регистрации.</param>
    /// <exception cref="AlreadyExistsException"> Пользователь уже есть в системе.</exception>
    /// <exception cref="NotFoundException"> Роль не найдена в бд. </exception>
    public async Task ExecuteAsync(RegisterUserRequest model)
    {
        var existingUser = await userService.FindByEmailAsync(model.email);
        if (existingUser != null)
        {
            throw new AlreadyExistsException("User already exists");
        }

        var role = await roleService.GetByNameAsync(model.role);

        var hashedPassword = passwordHasher.Hash(model.password);

        var user = new UserDto
        {
            Id = Guid.NewGuid(),
            FirstName = model.firstName,
            LastName = model.lastName,
            Email = model.email,
            Password = hashedPassword,
        };

        await userService.CreateAsync(user);

        var userRole = new UserRoleMembersDto
        {
            UserId = user.Id,
            RoleId = role.Id,
        };

        await userRoleMembersService.CreateAsync(userRole);
    }
}
