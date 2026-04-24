using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;
namespace RUSAL.MetalTapping.DAL.Repositories;

public class PotGroupRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<PotGroup, PotGroupModel>(context, mapper), 
        IPotGroupRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<PotGroup>> GetByBuildingIdsAsync(Guid buildingId)
    {
        var entities = await _context.PotGroupModels
            .Where(pg => pg.BuildingId == buildingId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PotGroup>>(entities);
    }
}