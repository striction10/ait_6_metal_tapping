using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class PotReglamentRepository(AppDbContext context) 
    : GenericRepository<PotReglament>(context), IPotReglamentRepository
{
    private readonly AppDbContext context = context;

    public async Task<IEnumerable<PotReglament>> GetByReglamentAndBuildingWithDeviationsAsync(
        Guid reglamentId,
        Guid buildingId)
    {
        return await context.PotReglaments
            .Include(pr => pr.Pot)
            .Include(pr => pr.Deviations)
                .ThenInclude(d => d.DeviationValues)
            .Where(pr => pr.ReglamentId == reglamentId &&
                        pr.Pot.BuildingId == buildingId)
            .ToListAsync();
    }
}
