using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class PotGroupRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<PotGroupDto, PotGroup>(context, mapper), 
        IPotGroupRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<PotGroupDto>> GetByBuildingIdsAsync(Guid buildingId)
    {
        var entities = await _context.PotGroupModels
            .Where(pg => pg.BuildingId == buildingId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PotGroupDto>>(entities);
    }
}