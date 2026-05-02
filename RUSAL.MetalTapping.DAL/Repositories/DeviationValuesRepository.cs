using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class DeviationValuesRepository(AppDbContext context)
    : GenericRepository<DeviationValues>(context), IDeviationValuesRepository
{
    private readonly AppDbContext context = context;

    public async Task<IEnumerable<DeviationValues>> GetDeviationValuesWithDeviationId(Guid id)
    {
        return await context.DeviationValues
            .Where(x => x.DeviationId == id)
            .ToListAsync();
    }
}
