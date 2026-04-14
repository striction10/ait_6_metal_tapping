using RUSAL.MetalTapping.BLL.Domain.Entities;

namespace RUSAL.MetalTapping.BLL.Domain.Interfaces
{
    public interface IShiftRepository : IGenericRepository<Shift>
    {
        Task<IEnumerable<Shift?>> GetCurrentShifts();
        Task<IEnumerable<Shift?>> GetNextShifts();
        Task<Shift?> GetNextShiftForBuilding(Guid buildingId, DateTime fromDate);
        Task<Shift?> GetByBuildingId(Guid buildingId);
    }
}