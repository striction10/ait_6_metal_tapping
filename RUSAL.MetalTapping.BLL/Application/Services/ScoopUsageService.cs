using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис для работы с данными об использовании ковшей.
/// </summary>
/// <param name="scoopUsageRepository">Репозиторий использования ковшей.</param>
/// <param name="mapper">Маппер объектов.</param>
public class ScoopUsageService(
    IScoopUsageRepository scoopUsageRepository,
    IMapper mapper)
{
    private readonly IScoopUsageRepository scoopUsageRepository = scoopUsageRepository;
    private readonly IMapper mapper = mapper;

    /// <summary>
    /// Получение записи об использовании ковша по идентификатору ковша.
    /// </summary>
    /// <param name="scoopId"> Идентификатор ковша. </param>
    /// <returns> DTO записи об использовании ковша. </returns>
    public async Task<ScoopUsageDto> GetByScoopIdAsync(Guid scoopId)
    {
        var entity = EnsureFound(
            await scoopUsageRepository.GetByScoopIdAsync(scoopId),
            $"Scoop usage with scoop {scoopId} was not found");

        return mapper.Map<ScoopUsageDto>(entity);
    }

    /// <summary>
    /// Создание записи об использовании ковша.
    /// </summary>
    /// <param name="dto"> DTO записи о использовании ковша. </param>
    public async Task CreateAsync(ScoopUsageDto dto)
    {
        var entity = mapper.Map<ScoopUsage>(dto);

        await scoopUsageRepository.CreateAsync(entity);
    }

    /// <summary>
    /// Обновление записи об использовании ковша.
    /// </summary>
    /// <param name="dto"> DTO записи об использовании ковша. </param>
    public async Task UpdateAsync(ScoopUsageDto dto)
    {
        var entity = await scoopUsageRepository.GetByIdAsync(dto.Id);

        mapper.Map(dto, entity);

        await scoopUsageRepository.SaveChangesAsync();
    }
}
