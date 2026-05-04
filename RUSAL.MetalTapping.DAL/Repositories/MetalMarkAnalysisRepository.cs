using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class MetalMarkAnalysisRepository(AppDbContext context)
    : GenericRepository<MetalMarkAnalysis>(context), IMetalMarkAnalysisRepository
{
    public async Task<MetalMarkAnalysis?> GetMetalMarkAnalysisWithPotIdAsync(Guid id)
    {
        return await context.MetalMarkAnalyses
            .Include(ma => ma.MetalMark)
            .Where(ma => ma.PotId == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<MetalMarkAnalysis?>> GetMetalMarkAnalysisWithPotIdsAsync(IEnumerable<Guid> potIds)
    {
        return await context.MetalMarkAnalyses
            .Include(ma => ma.MetalMark)
            .Where(ma => potIds.Contains(ma.PotId))
            .ToListAsync();
    }

    public async Task<IEnumerable<MetalMarkAnalysisValue>> GetValuesByAnalysisIdAsync(Guid analysisId)
    {
        return await context.MetalMarkAnalysisValues
            .Where(v => v.MetalMarkAnalysisId == analysisId)
            .ToListAsync();
    }
}
