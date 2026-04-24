using RUSAL.MetalTapping.BLL.Application.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class PotService(IGenericRepository<PotState> potStateRepository)
{
    private readonly IGenericRepository<PotState> _potStateRepository = potStateRepository;

    /// <summary>
    /// Создание DTO
    /// </summary>
    /// <param name="pots"> Электролизёры </param>
    /// <param name="calculated"> Расчетные задания электролизёров</param>
    /// <param name="analysis"> Анализы марки металла для электролизёров </param>
    /// <param name="marksByPot"> Распределение марок по электролизёрам </param>
    /// <param name="metalLevelByPot"> Распределение уровня металла по электролизёрам </param>
    /// <returns> Список электролизёров </returns>
    /// <exception cref="BusinessException"> Нет ЗПР для составления списка электролизёров </exception>
    public async Task<List<PotDto>> CreateAsync(
        IEnumerable<Pot> pots,
        IEnumerable<CalculatedTask> calculated,
        IEnumerable<MetalMarkAnalysis> analysis,
        Dictionary<Guid, MetalMarkAnalysis> marksByPot,
        Dictionary<Guid, double> metalLevelByPot)
    {
        var potDtos = new List<PotDto>();

        var calcByPot = calculated.ToDictionary(c => c.PotId);
        var analysisByPot = analysis.ToDictionary(a => a.PotId);

        foreach (var pot in pots)
        {
            var calc = calcByPot[pot.Id];
            var metalMarkAnalysis = analysisByPot[pot.Id];

            var potState = EnsureFound(
                await _potStateRepository.GetByIdAsync(pot.StateId),
                $"Pot state for pot {pot.Name} not found");

            var level = calc.RoundCalculatedTaskForPot
                ?? throw new BusinessException($"CalculatedMetalLevel is null for pot {pot.Name}");

            metalLevelByPot[pot.Id] = level;
            marksByPot[pot.Id] = metalMarkAnalysis;

            potDtos.Add(new PotDto
            {
                Id = pot.Id,
                Name = pot.Name,
                MetalLevel = level,
                MetalMarkId = metalMarkAnalysis.MetalMarkId,
                State = potState.Name
            });
        }

        return potDtos;
    }
}