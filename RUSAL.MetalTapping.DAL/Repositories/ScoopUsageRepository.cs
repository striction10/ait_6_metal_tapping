using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;
namespace RUSAL.MetalTapping.DAL.Repositories;

public class ScoopUsageRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<ScoopUsage, ScoopUsageModel>(context, mapper), 
        IScoopUsageRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<ScoopUsage?> GetByScoopIdAsync(Guid scoopId)
    {
        var entities = await _context.ScoopUsages
            .Where(su => su.ScoopId == scoopId)
            .FirstOrDefaultAsync();

        return _mapper.Map<ScoopUsage?>(entities);
    }
}