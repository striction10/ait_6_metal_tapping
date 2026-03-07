using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Interfaces
{
    public interface IPotReglamentRepository : IGenericService<PotReglament>
    {
        Task<IEnumerable<PotReglament?>> getByReglamentAndBuildingId(Guid reglamentId, Guid buildingId);
        Task<IEnumerable<PotReglament>> GetByReglamentAndBuildingWithDeviationsAsync(
            Guid reglamentId,
            Guid buildingId);
    }
}