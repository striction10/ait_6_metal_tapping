using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Interfaces;

public interface IShiftRepository : IGenericRepository<Shift>
{
    Task<IEnumerable<Shift?>> GetCurrentShifts();
    Task<IEnumerable<Shift?>> GetNextShifts();
    Task<Shift?> GetNextShiftForBuilding(Guid buildingId, DateTime fromDate);
    Task<Shift?> GetByBuildingId(Guid buildingId);
}