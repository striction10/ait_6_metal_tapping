using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;
namespace RUSAL.MetalTapping.DAL.Repositories;

public class TapTaskPotRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<TapTaskPot, TapTaskPotModel>(context, mapper), 
        ITapTaskPotRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<TapTaskPot?>> GetByTapTaskId(Guid tapTaskId)
    {
        var entities = await _context.TapTaskPots
            .Where(ttp => ttp.TapTaskId == tapTaskId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<TapTaskPot?>>(entities);
    }
}