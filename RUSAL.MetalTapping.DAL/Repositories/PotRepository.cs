using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class PotRepository(AppDbContext context)
    : GenericRepository<Pot>(context), IPotRepository
{
    public async Task<List<Pot>> GetFreshPotsWithAnalysisAsync(
        Guid buildingId,
        Guid metalMarkId,
        DateTime freshnessThreshold,
        int limit,
        CancellationToken cancellationToken = default)
    {
        return await context.Pots
            .Where(p => p.BuildingId == buildingId)
            .Where(p => context.MetalMarkAnalyses.Any(ma =>
                ma.PotId == p.Id && ma.MetalMarkId == metalMarkId && ma.DateOfReceipt >= freshnessThreshold))
            .Where(p => context.CalculatedTasks.Any(ct =>
                ct.PotId == p.Id && ct.CreatedAt >= freshnessThreshold))
            .Take(limit)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Pot>> GetPotsByGroupIdAsync(Guid groupId)
    {
        var potIds = await context.PotGroupsHistoryModels.
            Where(h => h.PotGroupId == groupId)
            .Select(h => h.PotId)
            .ToListAsync();

        return await context.Pots.
            Where(p => potIds.Contains(p.Id))
            .ToListAsync();
    }


}
