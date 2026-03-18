using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories
{
    public class DeviationValuesRepository : GenericRepository<DeviationValues, DeviationValuesModel>, IDeviationValuesRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DeviationValuesRepository(AppDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DeviationValues>> GetDeviationValuesWithDeviationId(Guid id)
        {
            var entities = await _context.DeviationValues
                .Where(x => x.DeviationId == id)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DeviationValues>>(entities);
        }
    }
}