using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class RoleRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<RoleDto, Role>(context, mapper), 
        IRoleRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<RoleDto?> GetByNameAsync(string name)
    {
        var entity = await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);

        return _mapper.Map<RoleDto>(entity);
    }

    public async Task<IEnumerable<RoleDto?>> GetUserRolesAsync(Guid userId)
    {
        var entities = await _context.UserRoleMembers
            .Where(x => x.UserId == userId)
            .Select(x => x.Role)
            .ToListAsync();

        return _mapper.Map<IEnumerable<RoleDto>>(entities);
    }
}