using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class MetalMarkAnalysisValueRepository(AppDbContext context)
    : GenericRepository<MetalMarkAnalysisValue>(context), IMetalMarkAnalysisValueRepository
{
    public async Task<IEnumerable<MetalMarkAnalysisValue>> GetValuesByAnalysisIdAsync(Guid analysisId)
    {
        return await context.MetalMarkAnalysisValues
            .Where(v => v.MetalMarkAnalysisId == analysisId)
            .ToListAsync();
    }
}
