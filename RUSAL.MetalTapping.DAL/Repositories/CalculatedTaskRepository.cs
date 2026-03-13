using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Interfaces;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Repositories
{
    public class CalculatedTaskRepository : GenericRepository<CalculatedTask>, ICalculatedTaskRepository
    {
        private readonly AppDbContext _context;

        public CalculatedTaskRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CalculatedTask?> GetCalculatedTaskWithPotIdAsync(Guid id)
        {
            return await _context.CalculatedTasks
                .Where(t => t.PotId == id)
                .OrderByDescending(t => t.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}