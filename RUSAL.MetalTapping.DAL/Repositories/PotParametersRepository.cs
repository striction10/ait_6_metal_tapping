using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Interfaces;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Repositories
{
    public class PotParametersRepository : GenericRepository<PotParameter>, IPotParametersRepository
    {
        private readonly AppDbContext _context;

        public PotParametersRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<PotParameter>> GetPotParametersWithGroupId(Guid id)
        {
            return await _context.PotParameters
                .Include(pp => pp.Group)
                .Where(pp => pp.Group.Id == id)
                .ToListAsync();
        }
    }
}
