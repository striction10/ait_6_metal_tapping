using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис для работы с отклонениями уровня металла.
/// </summary>
/// <param name="deviationRepository">Репозиторий отклонений.</param>
/// <param name="mapper">Маппер объектов.</param>
public class DeviationService(
    IDeviationRepository deviationRepository,
    IMapper mapper)
{
    private readonly IDeviationRepository deviationRepository = deviationRepository;
    private readonly IMapper mapper = mapper;

    /// <summary>
    /// Получение значения отклонения по идентификатору электролизёра.
    /// </summary>
    /// <param name="potId"> Идентификатор электролизёра. </param>
    /// <returns> DTO записи отклонения. </returns>
    public async Task<DeviationDto> GetWithPotIdAsync(Guid potId)
    {
        var entity = EnsureFound(
            await deviationRepository.GetDeviationWithPotIdAsync(potId),
            $"Deviation with pot {potId} was not found");

        return mapper.Map<DeviationDto>(entity);
    }

    /// <summary>
    /// Обновление записи об отклонении в базе данных.
    /// </summary>
    /// <param name="dto"> DTO записи отклонения. </param>
    public async Task UpdateAsync(DeviationDto dto)
    {
        var entity = await deviationRepository.GetByIdAsync(dto.Id);

        mapper.Map(dto, entity);

        await deviationRepository.SaveChangesAsync();
    }
}
