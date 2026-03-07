using System.Linq.Expressions;

namespace RUSAL.MetalTapping.DAL.Interfaces
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        Task CreateAsync(TEntity item);
        Task<TEntity?> FindByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAsync();
        Task RemoveAsync(TEntity item);
        Task UpdateAsync(TEntity item);
    }
}