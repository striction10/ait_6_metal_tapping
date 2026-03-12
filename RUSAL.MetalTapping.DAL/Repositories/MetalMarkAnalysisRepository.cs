using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Interfaces;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Repositories
{
    public class MetalMarkAnalysisRepository : GenericRepository<MetalMarkAnalysis>, IMetalMarkAnalysisRepository
    {
        private readonly AppDbContext _context;

        public MetalMarkAnalysisRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<MetalMarkAnalysis?> GetMetalMarkAnalysisWithPotIdAsync(Guid id) 
        {
            return await _context.MetalMarkAnalyses
                .Include(ma => ma.MetalMark)
                .Where(ma => ma.PotId == id)
                .FirstOrDefaultAsync();
        }
    }
}
