using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class ExternalDataService(
    IExternalDataRepository externalDataRepository,
    IMapper mapper)
{
    private readonly IExternalDataRepository _externalDataRepository = externalDataRepository;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Получение записи о внешних данных из базы данных по идентификатору электролизёра 
    /// </summary>
    /// <param name="potId"> Идентификатор электролизёра </param>
    /// <returns> DTO записи о внешних данных </returns>
    public async Task<ExternalDataDto?> GetByPotId(Guid potId)
    {
        var entity = EnsureFound(await _externalDataRepository.GetExternalDataWithPotId(potId),
            $"External data with pot {potId} was not found");

        return _mapper.Map<ExternalDataDto?>( entity);
    }
}