using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
using RUSAL.MetalTapping.DAL.Contexts;
namespace RUSAL.MetalTapping.DAL.Repositories;

public class ScoopUsageRepository(AppDbContext context) 
    : GenericRepository<ScoopUsage>(context), IScoopUsageRepository
{
    private readonly AppDbContext _context = context;

    public async Task<ScoopUsage?> GetByScoopIdAsync(Guid scoopId)
    {
        return await _context.ScoopUsages
            .Where(su => su.ScoopId == scoopId)
            .FirstOrDefaultAsync();
    }
}