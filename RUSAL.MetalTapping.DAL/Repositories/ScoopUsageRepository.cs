using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class ScoopUsageRepository(AppDbContext context)
    : GenericRepository<ScoopUsage>(context), IScoopUsageRepository
{
    public async Task<ScoopUsage?> GetByScoopIdAsync(Guid scoopId)
    {
        return await context.ScoopUsages
            .Where(su => su.ScoopId == scoopId)
            .FirstOrDefaultAsync();
    }
}
