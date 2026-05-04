using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class MetalMarkRepository(AppDbContext context)
    : GenericRepository<MetalMark>(context), IMetalMarkRepository
{
    public async Task<MetalMark?> GetByNameAsync(string name)
    {
        return await context.MetalMarks.FirstOrDefaultAsync(mm => mm.Name == name);
    }
}
