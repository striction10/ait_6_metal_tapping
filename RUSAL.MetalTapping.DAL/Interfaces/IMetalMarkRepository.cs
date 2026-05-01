using RUSAL.MetalTapping.DAL.Entities;
namespace RUSAL.MetalTapping.DAL.Interfaces;

public interface IMetalMarkRepository : IGenericRepository<MetalMark>
{
    Task<MetalMark?> GetByNameAsync(string name);
}
