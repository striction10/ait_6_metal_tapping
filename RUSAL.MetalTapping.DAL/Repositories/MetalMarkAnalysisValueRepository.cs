using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
namespace RUSAL.MetalTapping.DAL.Repositories;

public class MetalMarkAnalysisValueRepository(AppDbContext context)
    : GenericRepository<MetalMarkAnalysisValue>(context), IMetalMarkAnalysisValueRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<MetalMarkAnalysisValue>> GetValuesByAnalysisIdAsync(Guid analysisId)
    {
        return await _context.MetalMarkAnalysisValues
            .Where(v => v.MetalMarkAnalysisId == analysisId)
            .ToListAsync();
    }
}