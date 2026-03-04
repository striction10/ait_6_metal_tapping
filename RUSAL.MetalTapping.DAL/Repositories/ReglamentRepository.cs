using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Interfaces;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Repositories
{
    public class ReglamentRepository : GenericRepository<Reglament>, IReglamentRepository
    {
        private readonly AppDbContext _context;

        public ReglamentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Reglament?> GetNewReglament()
        {
            var now = DateTime.Now;
            return await _context.Reglaments
                .Where(r => r.DateStart <= now && r.DateStop >= now)
                .OrderByDescending(r => r.DateStart)
                .FirstOrDefaultAsync();
        }
    }
}