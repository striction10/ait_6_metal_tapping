using RUSAL.MetalTapping.BLL.Domain.Entities;

namespace RUSAL.MetalTapping.BLL.Domain.Interfaces
{
    public interface IReglamentRepository : IGenericRepository<Reglament>
    {
        Task<Reglament?> GetNewReglament();
    }
}
