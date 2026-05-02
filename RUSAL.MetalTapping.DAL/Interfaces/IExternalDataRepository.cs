using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Interfaces;

public interface IExternalDataRepository : IGenericRepository<ExternalData>
{
    Task<ExternalData?> GetExternalDataWithPotId(Guid id);
}
