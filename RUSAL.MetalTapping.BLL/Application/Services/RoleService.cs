using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис для работы с ролями пользователей.
/// </summary>
/// <param name="roleRepository">Репозиторий ролей.</param>
/// <param name="mapper">Маппер объектов.</param>
public class RoleService(
    IRoleRepository roleRepository,
    IMapper mapper)
{
    /// <summary>
    /// Получение роли по названию.
    /// </summary>
    /// <param name="name"> Название роли. </param>
    /// <returns> DTO роли. </returns>
    public async Task<RoleDto> GetByNameAsync(string name)
    {
        var entity = EnsureFound(
            await roleRepository.GetByNameAsync(name),
            $"Role {name} was not found");

        return mapper.Map<RoleDto>(entity);
    }
}
