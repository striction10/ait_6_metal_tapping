using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class PotParametersRepository(AppDbContext context)
        : GenericRepository<PotParameter>(context), IPotParametersRepository
{
    public async Task<IEnumerable<PotParameter>> GetPotParametersWithGroupId(Guid id)
    {
        return await context.PotParameters
            .Include(pp => pp.Group)
            .Where(pp => pp.Group.Id == id)
            .ToListAsync();
    }
}
