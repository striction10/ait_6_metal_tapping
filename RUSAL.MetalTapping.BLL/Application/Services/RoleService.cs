using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class RoleService(
    IRoleRepository roleRepository,
    IMapper mapper)
{
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Получение роли по названию
    /// </summary>
    /// <param name="name"> Название роли </param>
    /// <returns> DTO роли </returns>
    public async Task<RoleDto> GetByNameAsync(string name)
    {
        var entity = EnsureFound(await _roleRepository.GetByNameAsync(name),
            $"Role {name} was not found");

        return _mapper.Map<RoleDto>(entity);
    }
}