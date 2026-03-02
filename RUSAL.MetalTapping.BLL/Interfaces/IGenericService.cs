using System.Linq.Expressions;

namespace RUSAL.MetalTapping.BLL.Interfaces
{
    public interface IGenericService<TDto>
        where TDto : class
    {
        Task<TDto?> GetByIdAsync(int id);
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> CreateAsync(TDto dto);
        Task UpdateAsync(TDto dto);
        Task DeleteAsync(int id);
    }
}