using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Interfaces;

public interface ITapTaskPotRepository : IGenericRepository<TapTaskPot>
{
    Task<IEnumerable<TapTaskPot?>> GetByTapTaskId(Guid tapTaskId);
}