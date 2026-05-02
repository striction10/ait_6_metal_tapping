using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Interfaces;

public interface IMetalMarkAnalysisRepository : IGenericRepository<MetalMarkAnalysis>
{
    Task<MetalMarkAnalysis?> GetMetalMarkAnalysisWithPotIdAsync(Guid id);
    Task<IEnumerable<MetalMarkAnalysis?>> GetMetalMarkAnalysisWithPotIdsAsync(IEnumerable<Guid> potIds);
}
