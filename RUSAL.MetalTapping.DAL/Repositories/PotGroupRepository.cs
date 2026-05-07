using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class PotGroupRepository(AppDbContext context)
    : GenericRepository<PotGroup>(context), IPotGroupRepository
{
    public async Task<IEnumerable<PotGroup?>> GetByBuildingIdsAsync(Guid buildingId)
    {
        return await context.PotGroupModels
            .Where(pg => pg.BuildingId == buildingId)
            .ToListAsync();
    }

    public async Task<PotGroup?> GetByScoopIdAsync(Guid scoopId)
    {
        return await context.PotGroupModels
            .Where(pg => pg.ScoopId == scoopId)
            .FirstOrDefaultAsync();
    }
}
