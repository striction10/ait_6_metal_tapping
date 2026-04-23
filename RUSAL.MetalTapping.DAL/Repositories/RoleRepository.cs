using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.DAL.Repositories;

public class RoleRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<Role, RoleModel>(context, mapper), 
        IRoleRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Role?> GetByNameAsync(string name)
    {
        var entity = await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);

        return _mapper.Map<Role>(entity);
    }

    public async Task<IEnumerable<Role?>> GetUserRolesAsync(Guid userId)
    {
        var entities = await _context.UserRoleMembers
            .Where(x => x.UserId == userId)
            .Select(x => x.Role)
            .ToListAsync();

        return _mapper.Map<IEnumerable<Role>>(entities);
    }
}