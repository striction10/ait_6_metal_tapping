using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.BLL.Application.Services
{
    public class TapTaskService
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<TapTask> _tapTaskRepository;
        private readonly IGenericRepository<TapTaskPot> _tapTaskPotRepository;

        public TapTaskService(
            IGenericRepository<Order> orderRepository,
            IGenericRepository<TapTask> tapTaskRepository,
            IGenericRepository<TapTaskPot> tapTaskPotRepository)
        {
            _orderRepository = orderRepository;
            _tapTaskRepository = tapTaskRepository;
            _tapTaskPotRepository = tapTaskPotRepository;
        }

        public async Task CreateAsync(
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

            foreach (var segment in plan.Segments)
            {
                var tapTask = new TapTask
                {
                    Id = Guid.NewGuid(),
                    BuildingId = segment.BuildingId,
                    OrderId = order.Id,
                    ScoopId = segment.ScoopId
                };

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
        }
    }
}