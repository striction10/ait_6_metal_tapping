using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;
namespace RUSAL.MetalTapping.DAL.Repositories;

public class MetalMarkRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<MetalMark, MetalMarkModel>(context, mapper), 
        IMetalMarkRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<MetalMark?> GetByNameAsync(string name)
    {
        var entity = await _context.MetalMarks.FirstOrDefaultAsync(mm => mm.Name == name);

        return _mapper.Map<MetalMark>(entity);
    }
}