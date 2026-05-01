using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class MetalMarkService(
    IMetalMarkRepository metalMarkRepository,
    IMapper mapper)
{
    private readonly IMetalMarkRepository _metalMarkRepository = metalMarkRepository;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Получить имя марки металла
    /// </summary>
    /// <param name="metalMark"> Марка металла </param>
    /// <returns> Имя марки металла, иначе стандартное значение для отображения на клиенте </returns>
    public string ResolveMetalMarkName(MetalMarkDto? metalMark)
        => metalMark?.Name ?? "N/A";

    /// <summary>
    /// Получение списка всех марок металла
    /// </summary>
    /// <returns> DTO всех марок металла </returns>
    public async Task<IEnumerable<MetalMarkDto>> GetAllAsync()
    {
        var entities = await _metalMarkRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<MetalMarkDto>>(entities);
    }

    /// <summary>
    /// Получение марки металла по названию
    /// </summary>
    /// <param name="name"> Название марки металла </param>
    /// <returns> DTO марки металла </returns>
    public async Task<MetalMarkDto?> GetByNameAsync(string name)
    {
        var entity = EnsureFound(await _metalMarkRepository.GetByNameAsync(name),
            $"Metal mark with name {name} was not found");

        return _mapper.Map<MetalMarkDto?>(entity);
    }

    /// <summary>
    /// Получение марки металла по идентификатору
    /// </summary>
    /// <param name="id"> Идентификатор марки металла </param>
    /// <returns> DTO марки металла </returns>
    public async Task<MetalMarkDto?> GetByIdAsync(Guid id)
    {
        var entity = EnsureFound(await _metalMarkRepository.GetByIdAsync(id),
            $"Metal mark with id {id} was not found");

        return _mapper.Map<MetalMarkDto?>(entity);
    }
}