using RUSAL.MetalTapping.BLL.Domain.Entities;
namespace RUSAL.MetalTapping.BLL.Domain.Interfaces;

public interface IDeviationRepository : IGenericRepository<Deviation>
{
    Task<Deviation?> GetDeviationWithPotIdAsync(Guid potId);
}