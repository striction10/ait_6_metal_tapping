using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Interfaces
{
    public interface IPotParametersRepository : IGenericRepository<PotParameter>
    {
        Task<IEnumerable<PotParameter>> GetPotParametersWithGroupId(Guid id);
    }
}
