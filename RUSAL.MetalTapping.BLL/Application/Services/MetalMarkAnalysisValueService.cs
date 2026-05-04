using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис для работы с анализами марок металла в электролизёрах.
/// </summary>
/// <param name="metalMarkAnalysisRepository">Репозиторий анализов марок металла.</param>
/// <param name="mapper">Маппер объектов.</param>
public class MetalMarkAnalysisValueService(
    IMetalMarkAnalysisValueRepository metalMarkAnalysisValueRepository,
    IMapper mapper)
{
    /// <summary>
    /// Получение списка значений анализа марки металла по идентификатору анализа.
    /// </summary>
    /// <param name="analysisId"> Идентификатор анализа. </param>
    /// <returns> DTO значений анализа. </returns>
    public async Task<IEnumerable<MetalMarkAnalysisValueDto?>> GetByAnalysisIdAsync(Guid analysisId)
    {
        var entities = await metalMarkAnalysisValueRepository.GetValuesByAnalysisIdAsync(analysisId);

        return mapper.Map<IEnumerable<MetalMarkAnalysisValueDto?>>(entities);
    }
}
