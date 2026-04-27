using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class PotParametersRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<PotParametersDto, PotParameter>(context, mapper), 
        IPotParametersRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<PotParametersDto>> GetPotParametersWithGroupId(Guid id)
    {
        var entities = await _context.PotParameters
            .Include(pp => pp.Group)
            .Where(pp => pp.Group.Id == id)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PotParametersDto>>(entities);
    }
}
