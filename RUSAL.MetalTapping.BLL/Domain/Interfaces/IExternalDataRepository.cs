using RUSAL.MetalTapping.BLL.Domain.Entities;
namespace RUSAL.MetalTapping.BLL.Domain.Interfaces;

public interface IExternalDataRepository : IGenericRepository<ExternalData>
{
    Task<ExternalData?> GetExternalDataWithPotId(Guid id);
}