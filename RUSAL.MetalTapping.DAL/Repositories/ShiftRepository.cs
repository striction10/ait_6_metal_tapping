using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Repositories
{
    public class ShiftRepository : GenericRepository<Shift, ShiftModel>, IShiftRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ShiftRepository(AppDbContext context, IMapper mapper)
            : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Shift?>> GetCurrentShift()
        {
            var now = DateTime.UtcNow;

            var entities = await _context.Shifts
                .Where(s => s.BeginDate <= now && s.EndDate >= now)
                .ToListAsync();

            return _mapper.Map<IEnumerable<Shift?>>(entities);
        }

        public async Task<IEnumerable<Shift?>> GetNextShift()
        {
            var now = DateTime.UtcNow;

            var currentShift = await _context.Shifts
                .Where(s => s.BeginDate <= now && s.EndDate >= now)
                .FirstOrDefaultAsync();

            var nextShifts = await _context.Shifts
                .Where(s => s.BeginDate == currentShift.EndDate)
                .ToListAsync();

            return _mapper.Map<IEnumerable<Shift>>(nextShifts);
        }
    }
}