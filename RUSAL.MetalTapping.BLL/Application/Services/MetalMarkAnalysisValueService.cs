using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Interfaces;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class MetalMarkAnalysisValueService(
    IMetalMarkAnalysisValueRepository metalMarkAnalysisValueRepository,
    IMapper mapper)
{
    private readonly IMetalMarkAnalysisValueRepository _metalMarkAnalysisValueRepository = metalMarkAnalysisValueRepository;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Получение списка значений анализа марки металла по идентификатору анализа
    /// </summary>
    /// <param name="analysisId"> Идентификатор анализа </param>
    /// <returns> DTO значений анализа </returns>
    public async Task<IEnumerable<MetalMarkAnalysisValueDto?>> GetByAnalysisIdAsync(Guid analysisId)
    {
        var entities = await _metalMarkAnalysisValueRepository.GetValuesByAnalysisIdAsync(analysisId);

        return _mapper.Map<IEnumerable<MetalMarkAnalysisValueDto?>>(entities);
    }
}