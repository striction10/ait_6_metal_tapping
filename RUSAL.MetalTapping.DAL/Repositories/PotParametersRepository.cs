using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.DAL.Repositories;

public class PotParametersRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<PotParameters, PotParameterModel>(context, mapper), 
        IPotParametersRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<PotParameters>> GetPotParametersWithGroupId(Guid id)
    {
        var entities = await _context.PotParameters
            .Include(pp => pp.Group)
            .Where(pp => pp.Group.Id == id)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PotParameters>>(entities);
    }
}
