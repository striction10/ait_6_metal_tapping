using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Interfaces;

public interface IScoopUsageRepository : IGenericRepository<ScoopUsage>
{
    Task<ScoopUsage?> GetByScoopIdAsync(Guid scoopId);
}