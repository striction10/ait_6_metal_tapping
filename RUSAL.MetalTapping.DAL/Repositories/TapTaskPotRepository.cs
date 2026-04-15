using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Repositories
{
    public class TapTaskPotRepository : GenericRepository<TapTaskPot, TapTaskPotModel>, ITapTaskPotRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TapTaskPotRepository(AppDbContext context, IMapper mapper)
            : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TapTaskPot?>> GetByTapTaskId(Guid tapTaskId)
        {
            var entities = await _context.TapTaskPots
                .Where(ttp => ttp.TapTaskId == tapTaskId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TapTaskPot?>>(entities);
        }
    }
}