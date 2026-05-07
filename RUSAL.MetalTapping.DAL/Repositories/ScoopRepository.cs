using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class ScoopRepository(AppDbContext context) :
    GenericRepository<Scoop>(context), IScoopRepository
{
    public async Task<Scoop?> GetFirstAvailableScoopAsync(CancellationToken cancellationToken = default)
    {
        return await context.Scoops
            .Where(s => !context.ScoopUsages.Any(su => su.ScoopId == s.Id && su.BusyUntil > DateTime.UtcNow))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
