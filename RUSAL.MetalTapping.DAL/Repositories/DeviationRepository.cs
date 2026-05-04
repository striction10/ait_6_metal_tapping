using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class DeviationRepository(AppDbContext context)
    : GenericRepository<Deviation>(context), IDeviationRepository
{
    public async Task<Deviation?> GetDeviationWithPotIdAsync(Guid potId)
    {
        return await context.Deviations
            .Include(d => d.PotReglament)
            .FirstOrDefaultAsync(d => d.PotReglament.PotId == potId);
    }
}
