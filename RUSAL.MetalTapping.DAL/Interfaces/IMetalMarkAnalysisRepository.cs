using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Interfaces
{
    public interface IMetalMarkAnalysisRepository : IGenericRepository<MetalMarkAnalysis>
    {
        Task<MetalMarkAnalysis?> GetMetalMarkAnalysisWithPotIdAsync(Guid id);
    }
}
