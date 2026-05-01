using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using static RUSAL.MetalTapping.BLL.Domain.Guard;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.BLL.Application.Services;

public class DeviationValuesService(
    IDeviationValuesRepository deviationValuesRepository,
    IMapper mapper)
{
    private readonly IDeviationValuesRepository _deviationValuesRepository = deviationValuesRepository;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Получение значений отклонения по идентификатору отклонения
    /// </summary>
    /// <param name="deviationId"> Идентификатор отклонения </param>
    /// <returns> DTO значений отклонения </returns>
    public async Task<IEnumerable<DeviationValuesDto>> GetWithDeviationIdAsync(Guid deviationId) 
    {
        var entities = EnsureFound(await _deviationValuesRepository.GetDeviationValuesWithDeviationId(deviationId),
            $"Deviation values with deviation {deviationId} was not found");

        return _mapper.Map<IEnumerable<DeviationValuesDto>>(entities);
    }
}