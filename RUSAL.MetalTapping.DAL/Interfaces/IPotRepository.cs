using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Interfaces;

public interface IPotRepository : IGenericRepository<Pot>
{
    Task<IEnumerable<Pot>> GetPotsByGroupIdAsync(Guid groupId);
}
