using RUSAL.MetalTapping.BLL.Domain.Entities;
namespace RUSAL.MetalTapping.BLL.Domain.Interfaces;

public interface IMetalMarkRepository : IGenericRepository<MetalMark>
{
    Task<MetalMark?> GetByNameAsync(string name);
}
