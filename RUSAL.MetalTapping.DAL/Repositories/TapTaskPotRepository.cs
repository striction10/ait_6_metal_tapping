using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class TapTaskPotRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<TapTaskPotDto, TapTaskPot>(context, mapper), 
        ITapTaskPotRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<TapTaskPotDto?>> GetByTapTaskId(Guid tapTaskId)
    {
        var entities = await _context.TapTaskPots
            .Where(ttp => ttp.TapTaskId == tapTaskId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<TapTaskPotDto?>>(entities);
    }
}