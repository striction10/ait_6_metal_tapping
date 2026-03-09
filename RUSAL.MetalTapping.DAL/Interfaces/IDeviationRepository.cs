using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Interfaces
{
    public interface IDeviationRepository : IGenericRepository<Deviation>
    {
        Task<Deviation?> GetDeviationWithPotIdAsync(Guid potId);
    }
}