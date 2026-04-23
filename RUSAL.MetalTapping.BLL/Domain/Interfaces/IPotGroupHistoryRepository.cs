using RUSAL.MetalTapping.BLL.Domain.Entities;

namespace RUSAL.MetalTapping.BLL.Domain.Interfaces;

public interface IPotGroupHistoryRepository : IGenericRepository<PotGroupsHistory>
{
    Task<IEnumerable<Pot>> GetPotsByGroupIdAsync(Guid groupId);
}