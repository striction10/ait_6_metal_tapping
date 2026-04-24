using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.DAL.Repositories;

public class DeviationRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<Deviation, DeviationModel>(context, mapper), 
        IDeviationRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Deviation?> GetDeviationWithPotIdAsync(Guid id)
    {
        var entity = await _context.Deviations
            .Include(d => d.PotReglament)
            .FirstOrDefaultAsync(d => d.PotReglament.PotId == id);

        return _mapper.Map<Deviation>(entity);
    }
}