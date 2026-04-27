using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Interfaces;

public interface IPotParametersRepository : IGenericRepository<PotParameter>
{
    Task<IEnumerable<PotParameter>> GetPotParametersWithGroupId(Guid id);
}