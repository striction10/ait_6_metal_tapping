using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Interfaces;

public interface IDeviationRepository : IGenericRepository<Deviation>
{
    Task<Deviation?> GetDeviationWithPotIdAsync(Guid potId);
    Task<Deviation?> GetWithPotAndReglamentIdAsync(Guid reglamentId, Guid potId);
}
