using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис для работы с анализами марок металла в электролизёрах.
/// </summary>
/// <param name="metalMarkAnalysisRepository">Репозиторий анализов марок металла.</param>
/// <param name="mapper">Маппер объектов.</param>
public class MetalMarkAnalysisService(
    IMetalMarkAnalysisRepository metalMarkAnalysisRepository,
    IMapper mapper)
{
    private readonly IMetalMarkAnalysisRepository metalMarkAnalysisRepository = metalMarkAnalysisRepository;
    private readonly IMapper mapper = mapper;

    /// <summary>
    /// Получение записи об анализе марки металла внутри электролизёра по идентификатору электролизёра.
    /// </summary>
    /// <param name="potId"> Идентификатор электролизёра. </param>
    /// <returns> DTO анализа. </returns>
    public async Task<MetalMarkAnalysisDto?> GetByPotIdAsync(Guid potId)
    {
        var entity = EnsureFound(
            await metalMarkAnalysisRepository.GetMetalMarkAnalysisWithPotIdAsync(potId),
            $"Metal mark analysis with pot {potId} was not found");

        return mapper.Map<MetalMarkAnalysisDto?>(entity);
    }

    /// <summary>
    /// Получение списка записей об анализе марки металла внутри электролизёра по идентификаторам электролизёров.
    /// </summary>
    /// <param name="potIds"> Идентификатор электролизёров. </param>
    /// <returns> DTO записи об анализе. </returns>
    public async Task<IEnumerable<MetalMarkAnalysisDto?>> GetByPotIdsAsync(IEnumerable<Guid> potIds)
    {
        var entities = await metalMarkAnalysisRepository.GetMetalMarkAnalysisWithPotIdsAsync(potIds);

        return mapper.Map<IEnumerable<MetalMarkAnalysisDto?>>(entities);
    }
}
