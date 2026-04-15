using RUSAL.MetalTapping.BLL.Domain.Entities;

namespace RUSAL.MetalTapping.BLL.Domain.Interfaces
{
    public interface IScoopUsageRepository : IGenericRepository<ScoopUsage>
    {
        Task<ScoopUsage?> GetByScoopIdAsync(Guid scoopId);
    }
}