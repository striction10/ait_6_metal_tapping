using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.DAL.Models;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories
{
    public class PotReglamentRepository : GenericRepository<PotReglament, PotReglamentModel>, IPotReglamentRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public PotReglamentRepository(AppDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PotReglament?>> getByReglamentAndBuildingId(
            Guid reglamentId, 
            Guid buildingId)
        {
            var entities = await _context.PotReglaments
                .Include(pr => pr.Pot)
                .Where(pr => pr.ReglamentId == reglamentId && pr.Pot.BuildingId == buildingId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PotReglament>>(entities);
        }

        public async Task<IEnumerable<PotReglament>> GetByReglamentAndBuildingWithDeviationsAsync(
            Guid reglamentId,
            Guid buildingId)
        {
            var entities = await _context.PotReglaments
                .Include(pr => pr.Pot)
                .Include(pr => pr.Deviations)
                    .ThenInclude(d => d.DeviationValues)
                .Where(pr => pr.ReglamentId == reglamentId &&
                            pr.Pot.BuildingId == buildingId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PotReglament>>(entities);
        }
    }
}