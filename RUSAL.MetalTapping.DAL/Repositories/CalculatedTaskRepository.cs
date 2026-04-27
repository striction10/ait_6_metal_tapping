using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class CalculatedTaskRepository(
    AppDbContext context, IMapper mapper)
        : GenericRepository<CalculatedTask, CalculatedTask>(context, mapper), 
        ICalculatedTaskRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<CalculatedTaskDto?>> GetByPotIdsAsync(IEnumerable<Guid> potIds)
    {
        var entities = await _context.CalculatedTasks
            .Where(ct => potIds.Contains(ct.PotId))
            .ToListAsync();

        return _mapper.Map<IEnumerable<CalculatedTaskDto?>>(entities);
    }

    public async Task<CalculatedTaskDto?> GetCalculatedTaskWithPotIdAsync(Guid id)
    {
        var entity = await _context.CalculatedTasks
            .Where(t => t.PotId == id)
            .OrderByDescending(t => t.CreatedAt)
            .FirstOrDefaultAsync();

        return _mapper.Map<CalculatedTaskDto>(entity);
    }
}