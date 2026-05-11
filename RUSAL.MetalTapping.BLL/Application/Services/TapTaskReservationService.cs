using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.ViewModels;
using RUSAL.MetalTapping.BLL.Domain.DTOs;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис резервирования заданий на выливку.
/// </summary>
/// <param name="tapTaskService">Сервис для работы с заданиями на выливку.</param>
/// <param name="orderService">Сервис для работы с заказами.</param>
/// <param name="tapTaskPotService">Сервис для работы со связями заданий и электролизёров.</param>
public class TapTaskReservationService(
    TapTaskService tapTaskService,
    OrderService orderService,
    TapTaskPotService tapTaskPotService)
{
    /// <summary>
    /// Создание задания на выливку.
    /// </summary>
    /// <param name="plan"> План выливки. </param>
    /// <param name="orderRequest"> Заказ на выливку. </param>
    /// <param name="metalMarkId"> Идентификатор марки металла. </param>
    /// <param name="marksByPot"> Распределение анализов марки металла по электролизёрам. </param>
    /// <param name="metalLevelByPot"> Распределение уровня металла по электролизёрам. </param>
    /// <returns> Задания на выливку. </returns>
    public async Task<IEnumerable<TapTaskDto>> CreateTasksAsync(
        ExecutionPlanViewModel plan,
        OrderRequest orderRequest,
        Guid metalMarkId,
        Dictionary<Guid, MetalMarkAnalysisDto> marksByPot,
        Dictionary<Guid, double> metalLevelByPot)
    {
        var order = new OrderDto
        {
            Id = Guid.NewGuid(),
            WeightOfMetal = orderRequest.requiredMetalWeight,
            MetalmarkId = metalMarkId,
            DateOfOrder = DateTime.Now,
        };

        await orderService.CreateAsync(order);

        var tapTasks = new List<TapTaskDto>();

        foreach (var segment in plan.Segments)
        {
            var tapTask = new TapTaskDto
            {
                Id = Guid.NewGuid(),
                BuildingId = segment.BuildingId,
                OrderId = order.Id,
                ScoopId = segment.ScoopId,
            };

            tapTasks.Add(tapTask);

            await tapTaskService.CreateAsync(tapTask);

            foreach (var potId in segment.PotIds)
            {
                var tapTaskPot = new TapTaskPotDto
                {
                    Id = Guid.NewGuid(),
                    TapTaskId = tapTask.Id,
                    PotId = potId,
                    MetalMarkAnalysisId = marksByPot[potId].Id,
                    PotMetalWeigth = metalLevelByPot[potId],
                };

                await tapTaskPotService.CreateAsync(tapTaskPot);
            }
        }

        return tapTasks;
    }

    public async Task<IEnumerable<TapTaskDto>> CreateTasksForOrderAsync(
    Guid orderId,
    ExecutionPlanViewModel plan,
    Dictionary<Guid, MetalMarkAnalysisDto> marksByPot,
    Dictionary<Guid, double> metalLevelByPot)
    {
        var tapTasks = new List<TapTaskDto>();

        foreach (var segment in plan.Segments)
        {
            var tapTask = new TapTaskDto
            {
                Id = Guid.NewGuid(),
                BuildingId = segment.BuildingId,
                OrderId = orderId,
                ScoopId = segment.ScoopId,
            };

            tapTasks.Add(tapTask);
            await tapTaskService.CreateAsync(tapTask);

            foreach (var potId in segment.PotIds)
            {
                if (!marksByPot.TryGetValue(potId, out var analysis))
                {
                    continue;
                }

                if (!metalLevelByPot.TryGetValue(potId, out var weight))
                {
                    continue;
                }

                var tapTaskPot = new TapTaskPotDto
                {
                    Id = Guid.NewGuid(),
                    TapTaskId = tapTask.Id,
                    PotId = potId,
                    MetalMarkAnalysisId = analysis.Id,
                    PotMetalWeigth = weight,
                };

                await tapTaskPotService.CreateAsync(tapTaskPot);
            }
        }

        return tapTasks;
    }
}
