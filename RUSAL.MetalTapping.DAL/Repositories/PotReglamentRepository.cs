using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Interfaces;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Repositories
{
    public class PotReglamentRepository : GenericRepository<PotReglament>, IPotReglamentRepository
    {
        private readonly AppDbContext _context;

        public PotReglamentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PotReglament?>> getByReglamentId(Guid reglamentId)
        {
            return await _context.PotReglaments.Where(pr => pr.ReglamentId == reglamentId).ToListAsync();
        }
    }
}
