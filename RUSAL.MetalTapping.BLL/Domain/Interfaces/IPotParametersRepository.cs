using RUSAL.MetalTapping.BLL.Domain.Entities;
namespace RUSAL.MetalTapping.BLL.Domain.Interfaces;

public interface IPotParametersRepository : IGenericRepository<PotParameters>
{
    Task<IEnumerable<PotParameters>> GetPotParametersWithGroupId(Guid id);
}