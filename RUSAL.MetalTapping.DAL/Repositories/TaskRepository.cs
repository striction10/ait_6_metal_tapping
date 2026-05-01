using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
using RUSAL.MetalTapping.DAL.Contexts;
namespace RUSAL.MetalTapping.DAL.Repositories;

public class TaskRepository(
    AppDbContext context) 
    : GenericRepository<ShiftTask>(context), ITasksRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<ShiftTask?>> GetByShiftIdAsync(Guid shiftId)
    {
        return await _context.Tasks
            .Where(t => t.ShiftId == shiftId)
            .ToListAsync();
    }

    public async Task<IEnumerable<ShiftTask>> GetByBuildingAndDateRange(
        Guid buildingId,
        DateTime from,
        DateTime to)
    {
        return await _context.Tasks
            .Where(t =>
                t.Shift.BuildingId == buildingId &&
                t.LeadTime >= from &&
                t.LeadTime < to)
            .ToListAsync();
    }
}