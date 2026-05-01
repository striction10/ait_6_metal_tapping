using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Interfaces;
namespace RUSAL.MetalTapping.DAL.Repositories;

public class ReglamentRepository(AppDbContext context) : GenericRepository<Reglament>(context), IReglamentRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Reglament?> GetNewReglament()
    {
        var now = DateTime.Now;

        return await _context.Reglaments
            .Where(r => r.DateStart <= now && r.DateStop >= now)
            .OrderByDescending(r => r.DateStart)
            .FirstOrDefaultAsync();
    }
}