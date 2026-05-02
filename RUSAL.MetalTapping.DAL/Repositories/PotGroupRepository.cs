using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class PotGroupRepository(AppDbContext context) 
    : GenericRepository<PotGroup>(context), IPotGroupRepository
{
    private readonly AppDbContext context = context;

    public async Task<IEnumerable<PotGroup?>> GetByBuildingIdsAsync(Guid buildingId)
    {
        return await context.PotGroupModels
            .Where(pg => pg.BuildingId == buildingId)
            .ToListAsync();
    }
}
