using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
using RUSAL.MetalTapping.DAL.Contexts;
namespace RUSAL.MetalTapping.DAL.Repositories;

public class PotGroupRepository(AppDbContext context) 
    : GenericRepository<PotGroup>(context), IPotGroupRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<PotGroup?>> GetByBuildingIdsAsync(Guid buildingId)
    {
        return await _context.PotGroupModels
            .Where(pg => pg.BuildingId == buildingId)
            .ToListAsync();
    }
}