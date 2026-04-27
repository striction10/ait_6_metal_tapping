using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class DeviationValuesRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<DeviationValuesDto, DeviationValues>(context, mapper), 
        IDeviationValuesRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<DeviationValuesDto>> GetDeviationValuesWithDeviationId(Guid id)
    {
        var entities = await _context.DeviationValues
            .Where(x => x.DeviationId == id)
            .ToListAsync();

        return _mapper.Map<IEnumerable<DeviationValuesDto>>(entities);
    }
}