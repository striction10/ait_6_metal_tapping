using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Interfaces
{
    public interface IExternalDataRepository : IGenericRepository<ExternalData>
    {
        Task<ExternalData?> GetExternalDataWithPotId(Guid id);
    }
}
