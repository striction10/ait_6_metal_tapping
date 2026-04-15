using RUSAL.MetalTapping.BLL.Domain.Entities;

namespace RUSAL.MetalTapping.BLL.Domain.Interfaces
{
    public interface ITapTaskPotRepository : IGenericRepository<TapTaskPot>
    {
        Task<IEnumerable<TapTaskPot?>> GetByTapTaskId(Guid tapTaskId);
    }
}