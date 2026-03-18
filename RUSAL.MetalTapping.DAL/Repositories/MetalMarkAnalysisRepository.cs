using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;
using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories
{
    public class MetalMarkAnalysisRepository : GenericRepository<MetalMarkAnalysis, MetalMarkAnalysisModel>, IMetalMarkAnalysisRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MetalMarkAnalysisRepository(AppDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MetalMarkAnalysis?> GetMetalMarkAnalysisWithPotIdAsync(Guid id) 
        {
            var entity = await _context.MetalMarkAnalyses
                .Include(ma => ma.MetalMark)
                .Where(ma => ma.PotId == id)
                .FirstOrDefaultAsync();

            return _mapper.Map<MetalMarkAnalysis>(entity);
        }
    }
}
