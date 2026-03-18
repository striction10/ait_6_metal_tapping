using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories
{
    public class DeviationRepository : GenericRepository<Deviation, DeviationModel>, IDeviationRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DeviationRepository(AppDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Deviation?> GetDeviationWithPotIdAsync(Guid id)
        {
            var entity = await _context.Deviations
                .Include(d => d.PotReglament)
                .FirstOrDefaultAsync(d => d.PotReglament.PotId == id);

            return _mapper.Map<Deviation>(entity);
        }
    }
}