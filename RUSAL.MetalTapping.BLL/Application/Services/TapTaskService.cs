using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class TapTaskService(
    IGenericRepository<Order> orderRepository,
    IGenericRepository<TapTask> tapTaskRepository,
    IGenericRepository<TapTaskPot> tapTaskPotRepository)
{
    private readonly IGenericRepository<Order> _orderRepository = orderRepository;
    private readonly IGenericRepository<TapTask> _tapTaskRepository = tapTaskRepository;
    private readonly IGenericRepository<TapTaskPot> _tapTaskPotRepository = tapTaskPotRepository;

    /// <summary>
    /// Создание задания на выливку
    /// </summary>
    /// <param name="plan"> План выливки </param>
    /// <param name="orderRequest"> Заказ на выливку </param>
    /// <param name="metalMarkId"> Идентификатор марки металла </param>
    /// <param name="marksByPot"> Распределение анализов марки металла по электролизёрам </param>
    /// <param name="metalLevelByPot"> Распределение уровня металла по электролизёрам </param>
    /// <returns> Задания на выливку </returns>
    public async Task<IEnumerable<TapTask>> CreateAsync(
        ExecutionPlan plan,
        OrderRequest orderRequest,
        Guid metalMarkId,
        Dictionary<Guid, MetalMarkAnalysis> marksByPot,
        Dictionary<Guid, double> metalLevelByPot)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            WeightOfMetal = orderRequest.requiredMetalWeight,
            MetalmarkId = metalMarkId,
            DateOfOrder = DateTime.UtcNow
        };

        await _orderRepository.CreateAsync(order);

        var tapTasks = new List<TapTask>();

        foreach (var segment in plan.Segments)
        {
            var tapTask = new TapTask
            {
                Id = Guid.NewGuid(),
                BuildingId = segment.BuildingId,
                OrderId = order.Id,
                ScoopId = segment.ScoopId
            };

            tapTasks.Add(tapTask);

            await _tapTaskRepository.CreateAsync(tapTask);

            foreach (var potId in segment.PotIds)
            {
                var tapTaskPot = new TapTaskPot
                {
                    Id = Guid.NewGuid(),
                    TapTaskId = tapTask.Id,
                    PotId = potId,
                    MetalMarkAnalysisId = marksByPot[potId].Id,
                    PotMetalWeigth = metalLevelByPot[potId]
                };

                await _tapTaskPotRepository.CreateAsync(tapTaskPot);
            }
        }

        return tapTasks;
    }
}