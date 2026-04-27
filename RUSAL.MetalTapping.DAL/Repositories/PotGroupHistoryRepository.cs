using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class PotGroupHistoryRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<PotGroupsHistoryDto, PotGroupsHistory>(context, mapper), 
        IPotGroupHistoryRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<PotDto>> GetPotsByGroupIdAsync(Guid groupId)
    {
        var potIds = await _context.PotGroupsHistoryModels.
            Where(h => h.PotGroupId == groupId)
            .Select(h => h.PotId)
            .ToListAsync();

        var pots = await _context.Pots.
            Where(p => potIds.Contains(p.Id))
            .ToListAsync();

        return _mapper.Map<IEnumerable<PotDto>>(pots);
    }
}