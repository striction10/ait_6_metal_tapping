using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Interfaces
{
    public interface IReglamentRepository : IGenericRepository<Reglament>
    {
        Task<Reglament?> GetNewReglament();
    }
}
