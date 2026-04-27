using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Interfaces;

public interface IPotGroupHistoryRepository : IGenericRepository<PotGroupsHistory>
{
    Task<IEnumerable<Pot>> GetPotsByGroupIdAsync(Guid groupId);
}