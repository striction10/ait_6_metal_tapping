using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Interfaces;

public interface IRoleRepository : IGenericRepository<Role>
{
    Task<Role?> GetByNameAsync(string name);
    Task<IEnumerable<Role?>> GetUserRolesAsync(Guid userId);
}
