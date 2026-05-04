using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис для работы с заданиями на смену.
/// </summary>
/// <param name="tasksRepository">Репозиторий заданий на смену.</param>
/// <param name="mapper">Маппер объектов.</param>
public class TasksService(
    ITasksRepository tasksRepository,
    IMapper mapper)
{
    /// <summary>
    /// Получение задания на смену по идентификатору смены.
    /// </summary>
    /// <param name="shiftId"> Идентификатор смены. </param>
    /// <returns> DTO задания на смену. </returns>
    public async Task<IEnumerable<ShiftTaskDto?>> GetByShiftIdAsync(Guid shiftId)
    {
        var entities = EnsureFound(
            await tasksRepository.GetByShiftIdAsync(shiftId),
            $"Shift task for shift {shiftId} was not found");

        return mapper.Map<IEnumerable<ShiftTaskDto?>>(entities);
    }

    /// <summary>
    /// Получение задания на смену для корпуса по дате начала и окончания.
    /// </summary>
    /// <param name="buildingId"> Идентификатор корпуса. </param>
    /// <param name="from"> Дата начала смены. </param>
    /// <param name="to"> Дата окончания смены. </param>
    /// <returns> DTO задания на смену. </returns>
    public async Task<IEnumerable<ShiftTaskDto?>> GetByBuildingAndDateRangeAsync(Guid buildingId, DateTime from, DateTime to)
    {
        var entities = EnsureFound(
            await tasksRepository.GetByBuildingAndDateRange(buildingId, from, to),
            $"Shift task for building {buildingId} from {from} to {to} was not found");

        return mapper.Map<IEnumerable<ShiftTaskDto?>>(entities);
    }

    /// <summary>
    /// Создание задания на смену.
    /// </summary>
    /// <param name="dto"> DTO задания на смену. </param>
    public async Task CreateAsync(ShiftTaskDto dto)
    {
        var entity = mapper.Map<ShiftTask>(dto);

        await tasksRepository.CreateAsync(entity);
    }
}
