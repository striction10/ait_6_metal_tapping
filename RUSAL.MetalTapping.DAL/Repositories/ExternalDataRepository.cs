using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Interfaces;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Repositories
{
    public class ExternalDataRepository : GenericRepository<ExternalData>, IExternalDataRepository
    {
        private readonly AppDbContext _context;

        public ExternalDataRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ExternalData?> GetExternalDataWithPotId(Guid id)
        {
            return await _context.ExternalDatas
                .Include(ed => ed.Pot)
                .FirstOrDefaultAsync(ed => ed.PotId == id);
        }
    }
}