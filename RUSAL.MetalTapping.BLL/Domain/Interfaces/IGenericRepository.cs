namespace RUSAL.MetalTapping.BLL.Domain.Interfaces
{
    public interface IGenericRepository<TDomain>
    {
        Task<TDomain?> GetByIdAsync(Guid id);
        Task<IEnumerable<TDomain>> GetAllAsync();
        Task CreateAsync(TDomain entity);
        Task UpdateAsync(TDomain entity);
        Task DeleteAsync(Guid id);
    }
}