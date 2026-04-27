using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
using Task = RUSAL.MetalTapping.DAL.Entities.Task;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class TaskRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<ShiftTaskDto, Task>(context, mapper), 
        ITasksRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<ShiftTaskDto?>> GetByShiftIdAsync(Guid shiftId)
    {
        var entities = await _context.Tasks
            .Where(t => t.ShiftId == shiftId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ShiftTaskDto?>>(entities);
    }

    public async Task<IEnumerable<ShiftTaskDto>> GetByBuildingAndDateRange(
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

        return _mapper.Map<IEnumerable<ShiftTaskDto>>(entities);
    }
}