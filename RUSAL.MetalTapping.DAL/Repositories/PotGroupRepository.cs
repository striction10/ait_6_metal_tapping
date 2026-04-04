using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Repositories
{
    public class PotGroupRepository : GenericRepository<PotGroup, PotGroupModel>, IPotGroupRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PotGroupRepository(AppDbContext context, IMapper mapper)
            : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PotGroup>> GetByBuildingIdAsync(Guid buildingId)
        {
            var entities = await _context.PotGroupModels
                .Where(pg => pg.BuildingId == buildingId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PotGroup>>(entities);
        }
    }
}
