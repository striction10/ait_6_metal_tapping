using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class ShiftRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<ShiftDto, Shift>(context, mapper), 
        IShiftRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<ShiftDto?> GetByBuildingId(Guid buildingId)
    {
        var now = DateTime.UtcNow;

        var entity = await _context.Shifts
            .Where(s => s.BeginDate <= now && s.EndDate >= now)
            .Where(s => s.BuildingId == buildingId)
            .FirstOrDefaultAsync();

        return _mapper.Map<ShiftDto>(entity);
    }

    public async Task<IEnumerable<ShiftDto?>> GetCurrentShifts()
    {
        var now = DateTime.Now;

        var entities = await _context.Shifts
            .Where(s => s.BeginDate <= now && s.EndDate >= now)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ShiftDto?>>(entities);
    }

    public async Task<ShiftDto?> GetNextShiftForBuilding(Guid buildingId, DateTime fromDate)
    {
        var entity = await _context.Shifts
            .Where(s => s.BuildingId == buildingId)
            .Where(s => s.BeginDate > fromDate)
            .OrderBy(s => s.BeginDate)
            .FirstOrDefaultAsync();

        return _mapper.Map<ShiftDto?>(entity);
    }

    public async Task<IEnumerable<ShiftDto?>> GetNextShifts()
    {
        var now = DateTime.Now;

        var currentShift = await _context.Shifts
            .Where(s => s.BeginDate <= now && s.EndDate >= now)
            .FirstOrDefaultAsync();

        var entities = await _context.Shifts
            .Where(s => s.BeginDate > currentShift.EndDate)
            .OrderBy(s => s.BeginDate)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ShiftDto>>(entities);
    }
}