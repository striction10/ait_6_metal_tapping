using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;
        DbSet<TEntity> _DbSet;

        public GenericRepository(AppDbContext context) {
            _context = context;
            _DbSet = context.Set<TEntity>();
        }
        public async Task CreateAsync(TEntity item) 
        {
            await _DbSet.AddAsync(item);
            await _context.SaveChangesAsync();
        }
        public async Task<TEntity?> FindByIdAsync(int id)
        {
            return await _DbSet.FindAsync(id);
        }
        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await _DbSet.ToListAsync();
        }
        public async Task RemoveAsync(TEntity item)
        {
            _DbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
