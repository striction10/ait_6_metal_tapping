using RUSAL.MetalTapping.BLL.Domain.Entities;

namespace RUSAL.MetalTapping.BLL.Domain.Interfaces
{
    public interface IShiftRepository : IGenericRepository<Shift>
    {
        Task<IEnumerable<Shift?>> GetCurrentShift();
        Task<IEnumerable<Shift?>> GetNextShift();
    }
}