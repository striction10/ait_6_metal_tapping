using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.ViewModels;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class TapTaskReservationService(
    TapTaskService tapTaskService,
    OrderService orderService,
    TapTaskPotService tapTaskPotService)
{
    private readonly TapTaskService _tapTaskService = tapTaskService;
    private readonly OrderService _orderService = orderService;
    private readonly TapTaskPotService _tapTaskPotService = tapTaskPotService;

    /// <summary>
    /// Создание задания на выливку
    /// </summary>
    /// <param name="plan"> План выливки </param>
    /// <param name="orderRequest"> Заказ на выливку </param>
    /// <param name="metalMarkId"> Идентификатор марки металла </param>
    /// <param name="marksByPot"> Распределение анализов марки металла по электролизёрам </param>
    /// <param name="metalLevelByPot"> Распределение уровня металла по электролизёрам </param>
    /// <returns> Задания на выливку </returns>
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
            DateOfOrder = DateTime.UtcNow
        };

        await _orderService.CreateAsync(order);

        var tapTasks = new List<TapTaskDto>();

        foreach (var segment in plan.Segments)
        {
            var tapTask = new TapTaskDto
            {
                Id = Guid.NewGuid(),
                BuildingId = segment.BuildingId,
                OrderId = order.Id,
                ScoopId = segment.ScoopId
            };

            tapTasks.Add(tapTask);

            await _tapTaskService.CreateAsync(tapTask);

            foreach (var potId in segment.PotIds)
            {
                var tapTaskPot = new TapTaskPotDto
                {
                    Id = Guid.NewGuid(),
                    TapTaskId = tapTask.Id,
                    PotId = potId,
                    MetalMarkAnalysisId = marksByPot[potId].Id,
                    PotMetalWeigth = metalLevelByPot[potId]
                };

                await _tapTaskPotService.CreateAsync(tapTaskPot);
            }
        }

        return tapTasks;
    }
}