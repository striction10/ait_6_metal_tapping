using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.ViewModels;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.BLL.Application.Services;

public class TapTaskService(
    IGenericRepository<OrderDto> orderRepository,
    IGenericRepository<TapTaskDto> tapTaskRepository,
    IGenericRepository<TapTaskPotDto> tapTaskPotRepository)
{
    private readonly IGenericRepository<OrderDto> _orderRepository = orderRepository;
    private readonly IGenericRepository<TapTaskDto> _tapTaskRepository = tapTaskRepository;
    private readonly IGenericRepository<TapTaskPotDto> _tapTaskPotRepository = tapTaskPotRepository;

    /// <summary>
    /// Создание задания на выливку
    /// </summary>
    /// <param name="planViewModel"> План выливки </param>
    /// <param name="orderRequest"> Заказ на выливку </param>
    /// <param name="metalMarkId"> Идентификатор марки металла </param>
    /// <param name="marksByPot"> Распределение анализов марки металла по электролизёрам </param>
    /// <param name="metalLevelByPot"> Распределение уровня металла по электролизёрам </param>
    /// <returns> Задания на выливку </returns>
    public async Task<IEnumerable<TapTaskDto>> CreateAsync(
        ExecutionPlanViewModel planViewModel,
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

        await _orderRepository.CreateAsync(order);

        var tapTasks = new List<TapTaskDto>();

        foreach (var segment in planViewModel.Segments)
        {
            var tapTask = new TapTaskDto
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
                var tapTaskPot = new TapTaskPotDto
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