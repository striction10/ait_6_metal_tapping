using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Interfaces;

public interface ICalculatedTaskRepository : IGenericRepository<CalculatedTask>
{
    Task<CalculatedTask?> GetCalculatedTaskWithPotIdAsync(Guid potId);
    Task<IEnumerable<CalculatedTask?>> GetByPotIdsAsync(IEnumerable<Guid> potIds);
    Task<double?> GetFreshWeightForPotAsync(Guid potId, DateTime freshnessThreshold, CancellationToken ct = default);
}
