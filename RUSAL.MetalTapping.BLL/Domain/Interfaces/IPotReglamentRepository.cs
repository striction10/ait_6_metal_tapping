using RUSAL.MetalTapping.BLL.Domain.Entities;
namespace RUSAL.MetalTapping.BLL.Domain.Interfaces;

public interface IPotReglamentRepository : IGenericRepository<PotReglament>
{
    Task<IEnumerable<PotReglament?>> getByReglamentAndBuildingId(Guid reglamentId, Guid buildingId);
    Task<IEnumerable<PotReglament>> GetByReglamentAndBuildingWithDeviationsAsync(
        Guid reglamentId,
        Guid buildingId);
}