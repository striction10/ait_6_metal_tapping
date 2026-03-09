using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Interfaces;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Repositories
{
    public class DeviationValuesRepository : GenericRepository<DeviationValues>, IDeviationValuesRepository
    {
        private readonly AppDbContext _context;

        public DeviationValuesRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DeviationValues>> GetDeviationValuesWithDeviationId(Guid id)
        {
            return await _context.DeviationValues
                .Where(x => x.DeviationId == id)
                .ToListAsync();
        }
    }
}