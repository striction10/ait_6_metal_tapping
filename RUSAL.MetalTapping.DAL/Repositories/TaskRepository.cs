using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;
namespace RUSAL.MetalTapping.DAL.Repositories;

public class TaskRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<ShiftTask, TaskModel>(context, mapper), 
        ITasksRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<ShiftTask?>> GetByShiftIdAsync(Guid shiftId)
    {
        var entities = await _context.Tasks
            .Where(t => t.ShiftId == shiftId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ShiftTask?>>(entities);
    }

    public async Task<IEnumerable<ShiftTask>> GetByBuildingAndDateRange(
        Guid buildingId,
        DateTime from,
        DateTime to)
    {
        var entities = await _context.Tasks
            .Where(t =>
                t.Shift.BuildingId == buildingId &&
                t.LeadTime >= from &&
                t.LeadTime < to)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ShiftTask>>(entities);
    }
}