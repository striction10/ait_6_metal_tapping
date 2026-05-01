using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace RUSAL.MetalTapping.DAL.Repositories;

public class PotRepository(AppDbContext context)
    : GenericRepository<Pot>(context), IPotRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Pot>> GetPotsByGroupIdAsync(Guid groupId)
    {
        var potIds = await _context.PotGroupsHistoryModels.
            Where(h => h.PotGroupId == groupId)
            .Select(h => h.PotId)
            .ToListAsync();

        return await _context.Pots.
            Where(p => potIds.Contains(p.Id))
            .ToListAsync();
    }
}