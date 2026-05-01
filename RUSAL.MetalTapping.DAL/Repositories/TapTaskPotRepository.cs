using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
using RUSAL.MetalTapping.DAL.Contexts;
namespace RUSAL.MetalTapping.DAL.Repositories;

public class TapTaskPotRepository(AppDbContext context) 
    : GenericRepository<TapTaskPot>(context), ITapTaskPotRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<TapTaskPot?>> GetByTapTaskId(Guid tapTaskId)
    {
        return await _context.TapTaskPots
            .Where(ttp => ttp.TapTaskId == tapTaskId)
            .ToListAsync();
    }
}