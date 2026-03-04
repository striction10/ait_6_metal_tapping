using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Interfaces
{
    public interface IPotReglamentRepository : IGenericRepository<PotReglament>
    {
        Task<IEnumerable<PotReglament?>> getByReglamentId(Guid reglamentId);
    }
}
