using System.Linq.Expressions;

namespace RUSAL.MetalTapping.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task CreateAsync(TEntity item);
        Task<TEntity?> FindByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAsync();
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task RemoveAsync(TEntity item);
        Task UpdateAsync(TEntity item);
    }
}