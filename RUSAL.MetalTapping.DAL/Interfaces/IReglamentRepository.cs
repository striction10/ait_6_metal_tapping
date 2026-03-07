using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Interfaces
{
    public interface IReglamentRepository : IGenericService<Reglament>
    {
        Task<Reglament?> GetNewReglament();
    }
}
