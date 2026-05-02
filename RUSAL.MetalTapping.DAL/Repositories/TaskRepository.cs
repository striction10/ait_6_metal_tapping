using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class TaskRepository(
    AppDbContext context) 
    : GenericRepository<ShiftTask>(context), ITasksRepository
{
    private readonly AppDbContext context = context;

    public async Task<IEnumerable<ShiftTask?>> GetByShiftIdAsync(Guid shiftId)
    {
        return await context.Tasks
            .Where(t => t.ShiftId == shiftId)
            .ToListAsync();
    }

    public async Task<IEnumerable<ShiftTask>> GetByBuildingAndDateRange(
        Guid buildingId,
        DateTime from,
        DateTime to)
    {
        return await context.Tasks
            .Where(t =>
                t.Shift.BuildingId == buildingId &&
                t.LeadTime >= from &&
                t.LeadTime < to)
            .ToListAsync();
    }
}
