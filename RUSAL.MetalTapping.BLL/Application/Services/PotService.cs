using AutoMapper;
using RUSAL.MetalTapping.BLL.Application.ViewModels;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис для работы с электролизёрами.
/// </summary>
/// <param name="potStateRepository">Репозиторий состояний электролизёров.</param>
/// <param name="potRepository">Репозиторий электролизёров.</param>
/// <param name="mapper">Маппер объектов.</param>
public class PotService(
    IGenericRepository<PotState> potStateRepository,
    IPotRepository potRepository,
    IMapper mapper)
{
    /// <summary>
    /// Создание DTO.
    /// </summary>
    /// <param name="pots"> Электролизёры. </param>
    /// <param name="calculated"> Расчетные задания электролизёров.</param>
    /// <param name="analysis"> Анализы марки металла для электролизёров. </param>
    /// <param name="marksByPot"> Распределение марок по электролизёрам. </param>
    /// <param name="metalLevelByPot"> Распределение уровня металла по электролизёрам. </param>
    /// <returns> Список электролизёров. </returns>
    /// <exception cref="BusinessException"> Нет ЗПР для составления списка электролизёров. </exception>
    public async Task<List<PotViewModel>> CreateAsync(
        IEnumerable<PotDto> pots,
        IEnumerable<CalculatedTaskDto> calculated,
        IEnumerable<MetalMarkAnalysisDto> analysis,
        Dictionary<Guid, MetalMarkAnalysisDto> marksByPot,
        Dictionary<Guid, double> metalLevelByPot) // TODO: Вынести для пота поиск расчётного задания в этот сервис
    {
        var potDtos = new List<PotViewModel>();

        var calcByPot = calculated.ToDictionary(c => c.PotId);
        var analysisByPot = analysis.ToDictionary(a => a.PotId);

        foreach (var pot in pots)
        {
            var calc = calcByPot[pot.Id];
            var metalMarkAnalysis = analysisByPot[pot.Id];

            var potState = EnsureFound(
                await potStateRepository.GetByIdAsync(pot.StateId),
                $"Pot state for pot {pot.Name} not found");

            var level = calc.RoundCalculatedTaskForPot
                ?? throw new BusinessException($"CalculatedMetalLevel is null for pot {pot.Name}");

            metalLevelByPot[pot.Id] = level;
            marksByPot[pot.Id] = metalMarkAnalysis;

            potDtos.Add(new PotViewModel
            {
                Id = pot.Id,
                Name = pot.Name,
                MetalLevel = level,
                MetalMarkId = metalMarkAnalysis.MetalMarkId,
                State = potState.Name,
            });
        }

        return potDtos;
    }

    /// <summary>
    /// Получение электролизёра по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор электролизёра. </param>
    /// <returns> DTO электролизёра. </returns>
    public async Task<PotDto> GetByIdAsync(Guid id)
    {
        var entity = EnsureFound(
            await potRepository.GetByIdAsync(id),
            $"Pot with id {id} was not found");

        return mapper.Map<PotDto>(entity);
    }

    /// <summary>
    /// Получение списка электролизёров по идентификатору группы.
    /// </summary>
    /// <param name="groupId"> Идентификатор группы электролизёров. </param>
    /// <returns> DTO группы электролизёров. </returns>
    public async Task<IEnumerable<PotDto?>> GetByGroupIdAsync(Guid groupId)
    {
        var entities = EnsureFound(
            await potRepository.GetPotsByGroupIdAsync(groupId),
            $"Pots with group {groupId} was not found");

        return mapper.Map<IEnumerable<PotDto?>>(entities);
    }
}
