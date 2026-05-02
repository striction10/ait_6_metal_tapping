using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Interfaces;

public interface IPotReglamentRepository : IGenericRepository<PotReglament>
{
    Task<IEnumerable<PotReglament>> GetByReglamentAndBuildingWithDeviationsAsync(Guid reglamentId, Guid buildingId);
}
