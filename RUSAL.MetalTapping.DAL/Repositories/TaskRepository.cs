using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Repositories
{
    public class TaskRepository : GenericRepository<ShiftTask, TaskModel>, ITasksRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TaskRepository(AppDbContext context, IMapper mapper)
            : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShiftTask?>> GetByShiftIdAsync(Guid shiftId)
        {
            var entities = await _context.Tasks
                .Where(t => t.ShiftId == shiftId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ShiftTask?>>(entities);
        }
    }
}