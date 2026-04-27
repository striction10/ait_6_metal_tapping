using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class ScoopUsageRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<ScoopUsageDto, ScoopUsage>(context, mapper), 
        IScoopUsageRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<ScoopUsageDto?> GetByScoopIdAsync(Guid scoopId)
    {
        var entities = await _context.ScoopUsages
            .Where(su => su.ScoopId == scoopId)
            .FirstOrDefaultAsync();

        return _mapper.Map<ScoopUsageDto?>(entities);
    }
}