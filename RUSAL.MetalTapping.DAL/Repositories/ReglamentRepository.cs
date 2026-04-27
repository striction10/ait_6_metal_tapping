using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class ReglamentRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<ReglamentDto, Reglament>(context, mapper), 
        IReglamentRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<ReglamentDto?> GetNewReglament()
    {
        var now = DateTime.Now;
        var entity = await _context.Reglaments
            .Where(r => r.DateStart <= now && r.DateStop >= now)
            .OrderByDescending(r => r.DateStart)
            .FirstOrDefaultAsync();

        return _mapper.Map<ReglamentDto>(entity);
    }
}