using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис для работы с внешними данными электролизёров.
/// </summary>
/// <param name="externalDataRepository">Репозиторий внешних данных.</param>
/// <param name="mapper">Маппер объектов.</param>
public class ExternalDataService(
    IExternalDataRepository externalDataRepository,
    IMapper mapper)
{
    private readonly IExternalDataRepository externalDataRepository = externalDataRepository;
    private readonly IMapper mapper = mapper;

    /// <summary>
    /// Получение записи о внешних данных из базы данных по идентификатору электролизёра. 
    /// </summary>
    /// <param name="potId"> Идентификатор электролизёра. </param>
    /// <returns> DTO записи о внешних данных. </returns>
    public async Task<ExternalDataDto?> GetByPotId(Guid potId)
    {
        var entity = EnsureFound(
            await externalDataRepository.GetExternalDataWithPotId(potId),
            $"External data with pot {potId} was not found");

        return mapper.Map<ExternalDataDto?>( entity);
    }
}
