using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories
{
    public class GenericRepository<TDomain, TEntity> : IGenericRepository<TDomain>
        where TEntity : class
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext context, IMapper mapper) {
            _context = context;
            _dbSet = context.Set<TEntity>();
            _mapper = mapper;
        }
        public async Task<TDomain?> GetByIdAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            return _mapper.Map<TDomain>(entity);
        }

        public async Task<IEnumerable<TDomain>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
            return _mapper.Map<IEnumerable<TDomain>>(entities);
        }

        public async Task CreateAsync(TDomain domain)
        {
            var entity = _mapper.Map<TEntity>(domain);
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TDomain domain)
        {
            var entity = _mapper.Map<TEntity>(domain);
            _dbSet.Update(entity);
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
    }
}
