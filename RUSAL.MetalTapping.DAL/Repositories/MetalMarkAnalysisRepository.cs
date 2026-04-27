using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class MetalMarkAnalysisRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<MetalMarkAnalysisDto, MetalMarkAnalysis>(context, mapper), 
        IMetalMarkAnalysisRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<MetalMarkAnalysisDto?> GetMetalMarkAnalysisWithPotIdAsync(Guid id) 
    {
        var entity = await _context.MetalMarkAnalyses
            .Include(ma => ma.MetalMark)
            .Where(ma => ma.PotId == id)
            .FirstOrDefaultAsync();

        return _mapper.Map<MetalMarkAnalysisDto>(entity);
    }

    public async Task<IEnumerable<MetalMarkAnalysisDto?>> GetMetalMarkAnalysisWithPotIdsAsync(IEnumerable<Guid> potIds)
    {
        var entities = await _context.MetalMarkAnalyses
            .Include(ma => ma.MetalMark)
            .Where(ma => potIds.Contains(ma.PotId))
            .ToListAsync();

        return _mapper.Map<IEnumerable<MetalMarkAnalysisDto?>>(entities);
    }

    public async Task<IEnumerable<MetalMarkAnalysisValueDto>> GetValuesByAnalysisIdAsync(Guid analysisId)
    {
        var entities = await _context.MetalMarkAnalysisValues
            .Where(v => v.MetalMarkAnalysisId == analysisId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<MetalMarkAnalysisValueDto>>(entities);
    }
}