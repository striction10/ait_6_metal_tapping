using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class PotReglamentRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<PotReglamentDto, PotReglament>(context, mapper), 
        IPotReglamentRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<PotReglamentDto?>> getByReglamentAndBuildingId(
        Guid reglamentId, 
        Guid buildingId)
    {
        var entities = await _context.PotReglaments
            .Include(pr => pr.Pot)
            .Where(pr => pr.ReglamentId == reglamentId && pr.Pot.BuildingId == buildingId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PotReglamentDto>>(entities);
    }

    public async Task<IEnumerable<PotReglamentDto>> GetByReglamentAndBuildingWithDeviationsAsync(
        Guid reglamentId,
        Guid buildingId)
    {
        var entities = await _context.PotReglaments
            .Include(pr => pr.Pot)
            .Include(pr => pr.Deviations)
                .ThenInclude(d => d.DeviationValues)
            .Where(pr => pr.ReglamentId == reglamentId &&
                        pr.Pot.BuildingId == buildingId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PotReglamentDto>>(entities);
    }
}