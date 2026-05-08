using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Application.ViewModels;
using RUSAL.MetalTapping.BLL.Domain.DTOs;

namespace RUSAL.MetalTapping.BLL.Application.UseCases;

/// <summary>
/// Оркестратор сервисов получения списка всех пользователей.
/// </summary>
/// <param name="userService">Сервис работы с записями о пользователях.</param>
/// <param name="roleService">Сервис работы с записями о ролях.</param>
public class GetAllUsersUseCase(UserService userService,
    RoleService roleService)
{
    /// <summary>
    /// Получение всех пользователей в системе.
    /// </summary>
    /// <returns>ViewModel пользователей.</returns>
    public async Task<IEnumerable<UserViewModel>> ExecuteAsync()
    {
        var users = await userService.GetAllAsync();
        var userRoles = new Dictionary<Guid, RoleDto>();

        foreach (var user in users)
        {
            var role = await roleService.GetUserRoleAsync(user.Id);

            userRoles[user.Id] = role;
        }

        var usersDtos = users.Select(u => new UserViewModel
        {
            Email = u.Email,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Role = userRoles[u.Id]?.Name.ToString() ?? "Неизвестная роль",
        }).ToList();

        return usersDtos;
    }
}
