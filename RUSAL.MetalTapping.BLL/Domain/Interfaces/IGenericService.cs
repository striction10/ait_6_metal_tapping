namespace RUSAL.MetalTapping.BLL.Domain.Interfaces;

public interface IGenericService<TDto>
    where TDto : class
{
    Task<TDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<TDto> CreateAsync(TDto dto);
    Task UpdateAsync(TDto dto);
    Task DeleteAsync(Guid id);
}