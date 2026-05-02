using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис для работы со связями пользователей и ролей.
/// </summary>
/// <param name="userRoleMembersRepository">Репозиторий связей пользователей и ролей.</param>
/// <param name="mapper">Маппер объектов.</param>
public class UserRoleMembersService(
    IGenericRepository<UserRoleMembers> userRoleMembersRepository,
    IMapper mapper)
{
    private readonly IGenericRepository<UserRoleMembers> userRoleMembersRepository = userRoleMembersRepository;
    private readonly IMapper mapper = mapper;

    /// <summary>
    /// Создание записи о роли пользователя.
    /// </summary>
    /// <param name="dto"> DTO записи о роли пользователя. </param>
    public async Task CreateAsync(UserRoleMembersDto dto)
    {
        var entity = mapper.Map<UserRoleMembers>(dto);

        await userRoleMembersRepository.CreateAsync(entity);
    }
}
