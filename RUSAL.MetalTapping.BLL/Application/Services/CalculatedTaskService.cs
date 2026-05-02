using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Enums;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис для работы с расчётными заданиями электролизёров.
/// </summary>
/// <param name="calculatedTaskRepository"> Репозиторий расчётных заданий. </param>
/// <param name="mapper"> Маппер объектов. </param>
public class CalculatedTaskService(
    ICalculatedTaskRepository calculatedTaskRepository,
    IMapper mapper)
{
    private readonly ICalculatedTaskRepository calculatedTaskRepository = calculatedTaskRepository;
    private readonly IMapper mapper = mapper;

    /// <summary>
    /// Расчёт параметра "расчётное задание".
    /// </summary>
    /// <param name="amperage"> Выход по току у электролизёра. </param>
    /// <param name="avgAmperage"> Общая сила тока в корпусе. </param>
    /// <returns> Расчётное задание электролизёра. </returns>
    public double CalculatedTask(double amperage, double avgAmperage)
    {
        return Math.Round(
            amperage * avgAmperage * CalculateConstants.K / 100 * CalculateConstants.hoursCount,
            2);
    }

    /// <summary>
    /// Расчётное параметра "ЗПР".
    /// </summary>
    /// <param name="task"> Расчётное задание электролизёра. </param>
    /// <param name="castingRatio"> Коэффициент литья. </param>
    /// <returns> ЗПР электролизёра. </returns>
    public double CalculateRoundedTask(double task, int castingRatio)
    {
        return task * castingRatio / 100;
    }

    /// <summary>
    /// Создание записи о расчёте задания в базе данных.
    /// </summary>
    /// <param name="dto"> DTO расчётного задания. </param>
    public async Task CreateAsync(CalculatedTaskDto dto)
    {
        var entity = mapper.Map<CalculatedTask>(dto);

        await calculatedTaskRepository.CreateAsync(entity);
    }

    /// <summary>
    /// Получение записи о расчётном задании по идентификатору электролизёра.
    /// </summary>
    /// <param name="potId"> Идентификатор электролизёра. </param>
    public async Task<CalculatedTaskDto> GetByPotIdAsync(Guid potId)
    {
        var entity = EnsureFound(
            await calculatedTaskRepository.GetCalculatedTaskWithPotIdAsync(potId),
            $"Calculated task for pot {potId} was not found");

        return mapper.Map<CalculatedTaskDto>(entity);
    }

    /// <summary>
    /// Обновление записи о расчётном задании в базе данных.
    /// </summary>
    /// <param name="dto"> DTO расчётного задания. </param>
    public async Task UpdateAsync(CalculatedTaskDto dto)
    {
        var entity = EnsureFound(
            await calculatedTaskRepository.GetByIdAsync(dto.Id),
            $"Calculated task for pot {dto.Id} was not found");

        mapper.Map(dto, entity);

        await calculatedTaskRepository.SaveChangesAsync();
    }

    /// <summary>
    /// Получение записи о расчётных заданиях по списку идентификаторов электролизёров.
    /// </summary>
    /// <param name="potIds"> Идентификаторы электролизёров. </param>
    /// <returns> DTO расчётных заданий. </returns>
    public async Task<IEnumerable<CalculatedTaskDto?>> GetByPotIdsAsync(IEnumerable<Guid> potIds)
    {
        var entities = EnsureFound(
            await calculatedTaskRepository.GetByPotIdsAsync(potIds),
            $"Calculated tasks for ids was not found");

        return mapper.Map<IEnumerable<CalculatedTaskDto?>>(entities);
    }
}
