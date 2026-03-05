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

        public async Task<IEnumerable<PotReglament?>> getByReglamentAndBuildingId(Guid reglamentId, Guid buildingId)
        {
            return await _context.PotReglaments
                .Include(pr => pr.Pot)
                .Where(pr => pr.ReglamentId == reglamentId && pr.Pot.BuildingId == buildingId)
                .ToListAsync();
        }

        public async Task<IEnumerable<PotReglament>> GetByReglamentAndBuildingWithDeviationsAsync(
            Guid reglamentId,
            Guid buildingId)
        {
            return await _context.PotReglaments
                .Include(pr => pr.Pot)
                .Include(pr => pr.Deviations)
                    .ThenInclude(d => d.DeviationValues)
                .Where(pr => pr.ReglamentId == reglamentId &&
                            pr.Pot.BuildingId == buildingId)
                .ToListAsync();
        }
    }
}