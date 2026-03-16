using RUSAL.MetalTapping.BLL.Domain.Entities;

namespace RUSAL.MetalTapping.BLL.Domain.Interfaces
{
    public interface IMetalMarkAnalysisRepository : IGenericRepository<MetalMarkAnalysis>
    {
        Task<MetalMarkAnalysis?> GetMetalMarkAnalysisWithPotIdAsync(Guid id);
    }
}
