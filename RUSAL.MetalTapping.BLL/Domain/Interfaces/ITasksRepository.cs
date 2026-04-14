using RUSAL.MetalTapping.BLL.Domain.Entities;

namespace RUSAL.MetalTapping.BLL.Domain.Interfaces
{
    public interface ITasksRepository : IGenericRepository<ShiftTask>
    {
        Task<IEnumerable<ShiftTask?>> GetByShiftIdAsync(Guid shiftId);
    }
}