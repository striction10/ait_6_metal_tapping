using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;
using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.DAL.Repositories;

public class MetalMarkAnalysisRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<MetalMarkAnalysis, MetalMarkAnalysisModel>(context, mapper), 
        IMetalMarkAnalysisRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<MetalMarkAnalysis?> GetMetalMarkAnalysisWithPotIdAsync(Guid id) 
    {
        var entity = await _context.MetalMarkAnalyses
            .Include(ma => ma.MetalMark)
            .Where(ma => ma.PotId == id)
            .FirstOrDefaultAsync();

        return _mapper.Map<MetalMarkAnalysis>(entity);
    }

    public async Task<IEnumerable<MetalMarkAnalysis?>> GetMetalMarkAnalysisWithPotIdsAsync(IEnumerable<Guid> potIds)
    {
        var entities = await _context.MetalMarkAnalyses
            .Include(ma => ma.MetalMark)
            .Where(ma => potIds.Contains(ma.PotId))
            .ToListAsync();

        return _mapper.Map<IEnumerable<MetalMarkAnalysis?>>(entities);
    }

    public async Task<IEnumerable<MetalMarkAnalysisValue>> GetValuesByAnalysisIdAsync(Guid analysisId)
    {
        var entities = await _context.MetalMarkAnalysisValues
            .Where(v => v.MetalMarkAnalysisId == analysisId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<MetalMarkAnalysisValue>>(entities);
    }
}