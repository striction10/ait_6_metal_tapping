using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Enums;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис для работы с параметрами электролизёров.
/// </summary>
/// <param name="potParametersRepository">Репозиторий параметров электролизёров.</param>
/// <param name="mapper">Маппер объектов.</param>
public class PotParametersService(
    IPotParametersRepository potParametersRepository,
    IMapper mapper)
{
    private readonly IPotParametersRepository potParametersRepository = potParametersRepository;
    private readonly IMapper mapper = mapper;

   /// <summary>
   /// Получение параметра электролизёра.
   /// </summary>
   /// <param name="parameters"> Параметры электролизёра. </param>
   /// <param name="type"> Нужный тип параметра. </param>
   /// <returns> Нужный параметр электролизёра. </returns>
   /// <exception cref="Exception"> Параметр не найден. </exception>
    public double GetParameter(IEnumerable<PotParametersDto> parameters, PotParametersType type)
    {
        var param = parameters.FirstOrDefault(p => p.Type == type);

        if (param == null)
        {
            throw new NotFoundException($"Pot parameter {type} was not found");
        }

        return param.Value;
    }

    /// <summary>
    /// Получение списка параметров электролизёра по идентификатору группы параметров.
    /// </summary>
    /// <param name="groupId"> Идентификатор группы параметров электролизёра. </param>
    /// <returns> DTO параметров электролизёра. </returns>
    public async Task<IEnumerable<PotParametersDto>> GetByGroupId(Guid groupId)
    {
        var entities = EnsureFound(
            await potParametersRepository.GetPotParametersWithGroupId(groupId),
            $"Pot parameters with group {groupId} was not found");

        return mapper.Map<IEnumerable<PotParametersDto>>(entities);
    }
}
