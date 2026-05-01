using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
using RUSAL.MetalTapping.DAL.Contexts;
namespace RUSAL.MetalTapping.DAL.Repositories;

public class MetalMarkRepository(AppDbContext context)
    : GenericRepository<MetalMark>(context), IMetalMarkRepository
{
    private readonly AppDbContext _context = context;

    public async Task<MetalMark?> GetByNameAsync(string name)
    {
        return await _context.MetalMarks.FirstOrDefaultAsync(mm => mm.Name == name);
    }
}