using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;
using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.DAL.Repositories;

public class ExternalDataRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<ExternalData, ExternalDataModel>(context, mapper), 
        IExternalDataRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<ExternalData?> GetExternalDataWithPotId(Guid id)
    {
        var entity = await _context.ExternalDatas
            .Include(ed => ed.Pot)
            .FirstOrDefaultAsync(ed => ed.PotId == id);

        return _mapper.Map<ExternalData>(entity);
    }
}