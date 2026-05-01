using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Enums;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class CalculatedTaskService(
    ICalculatedTaskRepository calculatedTaskRepository,
    IMapper mapper)
{
    private readonly ICalculatedTaskRepository _calculatedTaskRepository = calculatedTaskRepository;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Расчёт параметра "расчётное задание"
    /// </summary>
    /// <param name="amperage"> Выход по току у электролизёра </param>
    /// <param name="avgAmperage"> Общая сила тока в корпусе </param>
    /// <returns> Расчётное задание электролизёра </returns>
    public double CalculatedTask(double amperage, double avgAmperage)
    {
        return Math.Round(
            amperage * avgAmperage * CalculateConstants.K / 100 * CalculateConstants.hoursCount,
            2);
    }

    /// <summary>
    /// Расчётное параметра "ЗПР"
    /// </summary>
    /// <param name="task"></param>
    /// <param name="castingRatio"></param>
    /// <returns> ЗПР электролизёра </returns>
    public double CalculateRoundedTask(double task, int castingRatio)
    {
        return task * castingRatio / 100;
    }

    /// <summary>
    /// Создание записи о расчёте задания в базе данных
    /// </summary>
    /// <param name="dto"></param>
    public async Task CreateAsync(CalculatedTaskDto dto)
    {
        var entity = _mapper.Map<CalculatedTask>(dto);

        await _calculatedTaskRepository.CreateAsync(entity);
    }

    /// <summary>
    /// Получение 
    /// </summary>
    /// <param name="potId"></param>
    /// <returns></returns>
    public async Task<CalculatedTaskDto> GetByPotIdAsync(Guid potId)
    {
        var entity = EnsureFound(await _calculatedTaskRepository.GetCalculatedTaskWithPotIdAsync(potId),
            $"Calculated task for pot {potId} was not found");

        return _mapper.Map<CalculatedTaskDto>(entity);
    }

    /// <summary>
    /// Обновление записи о расчётном задании в базе данных
    /// </summary>
    /// <param name="dto">  </param>
    /// <returns></returns>
    public async Task UpdateAsync(CalculatedTaskDto dto)
    {
        var entity = EnsureFound(await _calculatedTaskRepository.GetByIdAsync(dto.Id),
            $"Calculated task for pot {dto.Id} was not found");

        _mapper.Map(dto, entity);

        await _calculatedTaskRepository.SaveChangesAsync();
    }

    /// <summary>
    /// Получение записи о расчётных заданиях по списку идентификаторов электролизёров
    /// </summary>
    /// <param name="potIds"> Идентификаторы электролизёров </param>
    /// <returns> DTO расчётных заданий </returns>
    public async Task<IEnumerable<CalculatedTaskDto?>> GetByPotIdsAsync(IEnumerable<Guid> potIds)
    {
        var entities = EnsureFound(await _calculatedTaskRepository.GetByPotIdsAsync(potIds),
            $"Calculated tasks for ids was not found");

        return _mapper.Map<IEnumerable<CalculatedTaskDto?>>(entities);
    }
}