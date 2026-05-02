using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class RoleRepository(AppDbContext context) 
    : GenericRepository<Role>(context), IRoleRepository
{
    private readonly AppDbContext context = context;

    public async Task<Role?> GetByNameAsync(string name)
    {
        return await context.Roles.FirstOrDefaultAsync(r => r.Name == name);
    }

    public async Task<IEnumerable<Role?>> GetUserRolesAsync(Guid userId)
    {
        return await context.UserRoleMembers
            .Where(x => x.UserId == userId)
            .Select(x => x.Role)
            .ToListAsync();
    }
}
