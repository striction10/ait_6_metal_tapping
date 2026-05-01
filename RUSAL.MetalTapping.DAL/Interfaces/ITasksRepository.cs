using RUSAL.MetalTapping.DAL.Entities;
namespace RUSAL.MetalTapping.DAL.Interfaces;

public interface ITasksRepository : IGenericRepository<ShiftTask>
{
    Task<IEnumerable<ShiftTask?>> GetByShiftIdAsync(Guid shiftId);
    Task<IEnumerable<ShiftTask>> GetByBuildingAndDateRange(Guid buildingId, DateTime from, DateTime to);
}