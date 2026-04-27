namespace RUSAL.MetalTapping.DAL.Interfaces;

public interface ITasksRepository : IGenericRepository<Task>
{
    Task<IEnumerable<Task?>> GetByShiftIdAsync(Guid shiftId);
    Task<IEnumerable<Task>> GetByBuildingAndDateRange(Guid buildingId, DateTime from, DateTime to);
}