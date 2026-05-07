using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Interfaces;

public interface IPotGroupRepository : IGenericRepository<PotGroup>
{
    Task<IEnumerable<PotGroup?>> GetByBuildingIdsAsync(Guid buildingId);

    Task<PotGroup?> GetByScoopIdAsync(Guid scoopId);
}
