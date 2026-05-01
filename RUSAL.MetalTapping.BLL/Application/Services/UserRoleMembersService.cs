using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class UserRoleMembersService(
    IGenericRepository<UserRoleMembers> userRoleMembersRepository,
    IMapper mapper)
{
    private readonly IGenericRepository<UserRoleMembers> _userRoleMembersRepository = userRoleMembersRepository;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Создание записи о роли пользователя
    /// </summary>
    /// <param name="dto"> DTO записи о роли пользователя </param>
    public async Task CreateAsync(UserRoleMembersDto dto)
    {
        var entity = _mapper.Map<UserRoleMembers>(dto);

        await _userRoleMembersRepository.CreateAsync(entity);
    }
}