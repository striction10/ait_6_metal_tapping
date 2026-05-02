using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис для работы со значениями отклонений.
/// </summary>
/// <param name="deviationValuesRepository">Репозиторий значений отклонений.</param>
/// <param name="mapper">Маппер объектов.</param>
public class DeviationValuesService(
    IDeviationValuesRepository deviationValuesRepository,
    IMapper mapper)
{
    private readonly IDeviationValuesRepository deviationValuesRepository = deviationValuesRepository;
    private readonly IMapper mapper = mapper;

    /// <summary>
    /// Получение значений отклонения по идентификатору отклонения.
    /// </summary>
    /// <param name="deviationId"> Идентификатор отклонения. </param>
    /// <returns> DTO значений отклонения. </returns>
    public async Task<IEnumerable<DeviationValuesDto>> GetWithDeviationIdAsync(Guid deviationId) 
    {
        var entities = EnsureFound(
            await deviationValuesRepository.GetDeviationValuesWithDeviationId(deviationId),
            $"Deviation values with deviation {deviationId} was not found");

        return mapper.Map<IEnumerable<DeviationValuesDto>>(entities);
    }
}
