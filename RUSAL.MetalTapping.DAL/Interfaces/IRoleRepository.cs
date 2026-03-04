using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Interfaces
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role?> GetByNameAsync(string name);
    }
}