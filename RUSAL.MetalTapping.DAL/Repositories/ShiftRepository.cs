using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;
namespace RUSAL.MetalTapping.DAL.Repositories;

public class ShiftRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<Shift, ShiftModel>(context, mapper), 
        IShiftRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Shift?> GetByBuildingId(Guid buildingId)
    {
        var now = DateTime.UtcNow;

        var entity = await _context.Shifts
            .Where(s => s.BeginDate <= now && s.EndDate >= now)
            .Where(s => s.BuildingId == buildingId)
            .FirstOrDefaultAsync();

        return _mapper.Map<Shift>(entity);
    }

    public async Task<IEnumerable<Shift?>> GetCurrentShifts()
    {
        var now = DateTime.Now;

        var entities = await _context.Shifts
            .Where(s => s.BeginDate <= now && s.EndDate >= now)
            .ToListAsync();

        return _mapper.Map<IEnumerable<Shift?>>(entities);
    }

    public async Task<Shift?> GetNextShiftForBuilding(Guid buildingId, DateTime fromDate)
    {
        var entity = await _context.Shifts
            .Where(s => s.BuildingId == buildingId)
            .Where(s => s.BeginDate > fromDate)
            .OrderBy(s => s.BeginDate)
            .FirstOrDefaultAsync();

        return _mapper.Map<Shift?>(entity);
    }

    public async Task<IEnumerable<Shift?>> GetNextShifts()
    {
        var now = DateTime.Now;

        var currentShift = await _context.Shifts
            .Where(s => s.BeginDate <= now && s.EndDate >= now)
            .FirstOrDefaultAsync();

        var entities = await _context.Shifts
            .Where(s => s.BeginDate > currentShift.EndDate)
            .OrderBy(s => s.BeginDate)
            .ToListAsync();

        return _mapper.Map<IEnumerable<Shift>>(entities);
    }
}