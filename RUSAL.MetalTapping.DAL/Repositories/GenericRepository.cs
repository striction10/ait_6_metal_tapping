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
        public void Create(TEntity item) 
        {
            _DbSet.Add(item);
            _context.SaveChanges();
        }
        public TEntity FindById(int id)
        {
            return _DbSet.Find(id);
        }

        public IEnumerable<TEntity> Get() 
        {
            return _DbSet.ToList();
        }
        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate) 
        {
            return _DbSet.Where(predicate).ToList();
        }
        public void Remove(TEntity item)
        { 
            _DbSet.Remove(item);
            _context.SaveChanges();
        }
        public void Update(TEntity item) 
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
