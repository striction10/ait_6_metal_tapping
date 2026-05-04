using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис для работы с регламентами.
/// </summary>
/// <param name="reglamentRepository">Репозиторий регламентов.</param>
/// <param name="mapper">Маппер объектов.</param>
public class ReglamentService(IReglamentRepository reglamentRepository, IMapper mapper)
{
    /// <summary>
    /// Получение регламента по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор регламента. </param>
    /// <returns> DTO регламента. </returns>
    public async Task<ReglamentDto> GetByIdAsync(Guid id)
    {
        var entity = EnsureFound(
            await reglamentRepository.GetByIdAsync(id),
            $"Reglament with id {id} was not found");

        return mapper.Map<ReglamentDto>(entity);
    }

    /// <summary>
    /// Получение текущего действующего регламента.
    /// </summary>
    /// <returns> DTO действующего регламента. </returns>
    public async Task<ReglamentDto> GetCurrentReglament()
    {
        var entity = EnsureFound(
            await reglamentRepository.GetNewReglament(),
            "Current reglament was not found");

        return mapper.Map<ReglamentDto>(entity);
    }

    /// <summary>
    /// Получения списка всех регламентов.
    /// </summary>
    /// <returns> DTO регламентов. </returns>
    public async Task<IEnumerable<ReglamentDto>> GetAllAsync()
    {
        var entities = await reglamentRepository.GetAllAsync();

        return mapper.Map<IEnumerable<ReglamentDto>>(entities);
    }
}
