using RUSAL.MetalTapping.BLL.Domain.Entities;
namespace RUSAL.MetalTapping.BLL.Domain.Interfaces;

public interface IMetalMarkAnalysisRepository : IGenericRepository<MetalMarkAnalysis>
{
    Task<MetalMarkAnalysis?> GetMetalMarkAnalysisWithPotIdAsync(Guid id);
    Task<IEnumerable<MetalMarkAnalysis?>> GetMetalMarkAnalysisWithPotIdsAsync(IEnumerable<Guid> potIds);
    Task<IEnumerable<MetalMarkAnalysisValue>> GetValuesByAnalysisIdAsync(Guid analysisId);
}