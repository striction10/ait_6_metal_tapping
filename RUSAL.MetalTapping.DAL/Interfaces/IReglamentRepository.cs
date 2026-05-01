using RUSAL.MetalTapping.DAL.Entities;
namespace RUSAL.MetalTapping.DAL.Interfaces;

public interface IReglamentRepository : IGenericRepository<Reglament>
{
    Task<Reglament?> GetNewReglament();
}
