using RUSAL.MetalTapping.BLL.Domain.Entities;

namespace RUSAL.MetalTapping.BLL.Domain.Interfaces
{
    public interface IPotGroupRepository : IGenericRepository<PotGroup>
    {
        Task<IEnumerable<PotGroup>> GetByBuildingIdAsync(Guid buildingId);
    }
}