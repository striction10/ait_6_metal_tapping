using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class PotRepository(AppDbContext context)
    : GenericRepository<Pot>(context), IPotRepository
{
    private readonly AppDbContext context = context;

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
