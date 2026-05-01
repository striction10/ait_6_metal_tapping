using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
namespace RUSAL.MetalTapping.DAL.Repositories;

public class CalculatedTaskRepository(AppDbContext context)
    : GenericRepository<CalculatedTask>(context), ICalculatedTaskRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<CalculatedTask?>> GetByPotIdsAsync(IEnumerable<Guid> potIds)
    {
        return await _context.CalculatedTasks
            .Where(ct => potIds.Contains(ct.PotId))
            .ToListAsync();
    }

    public async Task<CalculatedTask?> GetCalculatedTaskWithPotIdAsync(Guid potId)
    {
        return await _context.CalculatedTasks
            .Where(t => t.PotId == potId)
            .OrderByDescending(t => t.CreatedAt)
            .FirstOrDefaultAsync();
    }
}