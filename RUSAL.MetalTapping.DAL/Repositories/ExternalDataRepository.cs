using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class ExternalDataRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<ExternalDataDto, ExternalData>(context, mapper), 
        IExternalDataRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<ExternalDataDto?> GetExternalDataWithPotId(Guid id)
    {
        var entity = await _context.ExternalDatas
            .Include(ed => ed.Pot)
            .FirstOrDefaultAsync(ed => ed.PotId == id);

        return _mapper.Map<ExternalDataDto>(entity);
    }
}