using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class TapTaskPotRepository(AppDbContext context)
    : GenericRepository<TapTaskPot>(context), ITapTaskPotRepository
{
    public async Task<IEnumerable<TapTaskPot?>> GetByTapTaskId(Guid tapTaskId)
    {
        return await context.TapTaskPots
            .Where(ttp => ttp.TapTaskId == tapTaskId)
            .ToListAsync();
    }
}
