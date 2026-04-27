using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class GenericRepository<TEntity>(
    AppDbContext context, IMapper mapper) 
        : IGenericRepository<TEntity>
        where TEntity : class
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task CreateAsync(TEntity entity)
    {
        var entity = _mapper.Map<TEntity>(domain);
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity domain)
    {
        var existingEntity = await _dbSet.FindAsync(domain.Id);
        if (existingEntity == null)
            throw new Exception("Entity not found");

        _mapper.Map(domain, existingEntity);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}