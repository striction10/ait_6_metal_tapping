using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories
{
    public class CalculatedTaskRepository : GenericRepository<CalculatedTask, CalculatedTaskModel>, ICalculatedTaskRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CalculatedTaskRepository(AppDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CalculatedTask?>> GetByPotIdsAsync(IEnumerable<Guid> potIds)
        {
            var entities = await _context.CalculatedTasks
                .Where(ct => potIds.Contains(ct.PotId))
                .ToListAsync();

            return _mapper.Map<IEnumerable<CalculatedTask?>>(entities);
        }

        public async Task<CalculatedTask?> GetCalculatedTaskWithPotIdAsync(Guid id)
        {
            var entity = await _context.CalculatedTasks
                .Where(t => t.PotId == id)
                .OrderByDescending(t => t.CreatedAt)
                .FirstOrDefaultAsync();

            return _mapper.Map<CalculatedTask>(entity);
        }
    }
}