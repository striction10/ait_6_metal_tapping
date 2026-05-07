using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Interfaces;

public interface IPotRepository : IGenericRepository<Pot>
{
    Task<IEnumerable<Pot>> GetPotsByGroupIdAsync(Guid groupId);
    Task<List<Pot>> GetFreshPotsWithAnalysisAsync(
        Guid buildingId, Guid metalMarkId, DateTime freshnessThreshold, int limit,
        CancellationToken cancellationToken = default);
}
