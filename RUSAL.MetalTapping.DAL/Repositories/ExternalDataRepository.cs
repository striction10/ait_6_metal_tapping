using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class ExternalDataRepository(AppDbContext context) 
    : GenericRepository<ExternalData>(context), IExternalDataRepository
{
    private readonly AppDbContext context = context;

    public async Task<ExternalData?> GetExternalDataWithPotId(Guid id)
    {
        return await context.ExternalDatas
            .Include(ed => ed.Pot)
            .FirstOrDefaultAsync(ed => ed.PotId == id);
    }
}
