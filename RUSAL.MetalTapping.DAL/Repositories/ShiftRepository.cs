using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class ShiftRepository(AppDbContext context) 
    : GenericRepository<Shift>(context), IShiftRepository
{
    private readonly AppDbContext context = context;

    public async Task<Shift?> GetByBuildingId(Guid buildingId)
    {
        var now = DateTime.UtcNow;

        return await context.Shifts
            .Where(s => s.BeginDate <= now && s.EndDate >= now)
            .Where(s => s.BuildingId == buildingId)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Shift?>> GetCurrentShifts()
    {
        var now = DateTime.Now;

        return await context.Shifts
            .Where(s => s.BeginDate <= now && s.EndDate >= now)
            .ToListAsync();
    }

    public async Task<Shift?> GetNextShiftForBuilding(Guid buildingId, DateTime fromDate)
    {
        return await context.Shifts
            .Where(s => s.BuildingId == buildingId)
            .Where(s => s.BeginDate > fromDate)
            .OrderBy(s => s.BeginDate)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Shift?>> GetNextShifts()
    {
        var now = DateTime.Now;

        var currentShift = await context.Shifts
            .Where(s => s.BeginDate <= now && s.EndDate >= now)
            .FirstOrDefaultAsync();

        return await context.Shifts
            .Where(s => s.BeginDate > currentShift.EndDate)
            .OrderBy(s => s.BeginDate)
            .ToListAsync();
    }
}
