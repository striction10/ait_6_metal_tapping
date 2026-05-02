using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Interfaces;

public interface IMetalMarkAnalysisValueRepository : IGenericRepository<MetalMarkAnalysisValue>
{
    Task<IEnumerable<MetalMarkAnalysisValue>> GetValuesByAnalysisIdAsync(Guid analysisId);
}
