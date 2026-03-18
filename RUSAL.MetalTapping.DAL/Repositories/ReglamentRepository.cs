using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories
{
    public class ReglamentRepository : GenericRepository<Reglament, ReglamentModel>, IReglamentRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ReglamentRepository(AppDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Reglament?> GetNewReglament()
        {
            var now = DateTime.Now;
            var entity = await _context.Reglaments
                .Where(r => r.DateStart <= now && r.DateStop >= now)
                .OrderByDescending(r => r.DateStart)
                .FirstOrDefaultAsync();

            return _mapper.Map<Reglament>(entity);
        }
    }
}