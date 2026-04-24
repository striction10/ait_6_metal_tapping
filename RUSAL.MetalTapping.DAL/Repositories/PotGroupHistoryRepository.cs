using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;
namespace RUSAL.MetalTapping.DAL.Repositories;

public class PotGroupHistoryRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<PotGroupsHistory, PotGroupsHistoryModel>(context, mapper), 
        IPotGroupHistoryRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<Pot>> GetPotsByGroupIdAsync(Guid groupId)
    {
        var potIds = await _context.PotGroupsHistoryModels.
            Where(h => h.PotGroupId == groupId)
            .Select(h => h.PotId)
            .ToListAsync();

        var pots = await _context.Pots.
            Where(p => potIds.Contains(p.Id))
            .ToListAsync();

        return _mapper.Map<IEnumerable<Pot>>(pots);
    }
}