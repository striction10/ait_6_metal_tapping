using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис для работы с состояниями ковшей.
/// </summary>
/// <param name="scoopStateRepository">Репозиторий состояний ковшей.</param>
/// <param name="mapper">Маппер объектов.</param>
public class ScoopStateService(
    IGenericRepository<ScoopState> scoopStateRepository,
    IMapper mapper)
{
    /// <summary>
    /// Получение состояние ковша по идентификатору состояния.
    /// </summary>
    /// <param name="scoopStateId"> Идентификатор состояния. </param>
    /// <returns> DTO состояния ковша. </returns>
    public async Task<ScoopStateDto?> GetByIdAsync(Guid scoopStateId)
    {
        var entity = EnsureFound(
            await scoopStateRepository.GetByIdAsync(scoopStateId),
            $"Scoop state with id {scoopStateId}");

        return mapper.Map<ScoopStateDto?>(entity);
    }
}
