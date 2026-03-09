using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Interfaces;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Repositories
{
    public class DeviationRepository : GenericRepository<Deviation>, IDeviationRepository
    {
        private readonly AppDbContext _context;

        public DeviationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Deviation?> GetDeviationWithPotIdAsync(Guid id)
        {
            return await _context.Deviations
                .Include(d => d.PotReglament)
                .FirstOrDefaultAsync(d => d.PotReglament.PotId == id);
        }
    }
}