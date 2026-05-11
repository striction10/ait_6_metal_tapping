using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Application.ViewModels;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;

public class OrderQueueWorker(IServiceProvider sp) : BackgroundService
{
    private readonly int _freshnessHours = 24;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = sp.CreateScope();

            var orderService = scope.ServiceProvider.GetRequiredService<OrderService>();
            var buildingService = scope.ServiceProvider.GetRequiredService<BuildingService>();
            var potGroupService = scope.ServiceProvider.GetRequiredService<PotGroupService>();
            var potService = scope.ServiceProvider.GetRequiredService<PotService>();
            var scoopService = scope.ServiceProvider.GetRequiredService<ScoopService>();
            var scoopStateService = scope.ServiceProvider.GetRequiredService<ScoopStateService>();
            var scoopUsageService = scope.ServiceProvider.GetRequiredService<ScoopUsageService>();
            var calculatedTaskService = scope.ServiceProvider.GetRequiredService<CalculatedTaskService>();
            var metalMarkAnalysisService = scope.ServiceProvider.GetRequiredService<MetalMarkAnalysisService>();
            var buildingInfoService = scope.ServiceProvider.GetRequiredService<BuildingService>();
            var planSelector = scope.ServiceProvider.GetRequiredService<CastingExecutionPlanService>();
            var tapTaskReservationService = scope.ServiceProvider.GetRequiredService<TapTaskReservationService>();
            var shiftAssignmentService = scope.ServiceProvider.GetRequiredService<ShiftAssignmentService>();

            var order = await orderService.GetNextPendingOrderAsync(stoppingToken);
            if (order == null)
            {
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                continue;
            }

            var buildings = await buildingService.GetAllAsync();
            var marksByPot = new Dictionary<Guid, MetalMarkAnalysisDto>();
            var metalLevelByPot = new Dictionary<Guid, double>();
            var buildingInfos = new List<BuildingMetalInfoViewModel>();

            foreach (var building in buildings)
            {
                var groups = await potGroupService.GetByBuildingIdAsync(building.Id);
                var groupDtos = new List<PotGroupViewModel>();

                foreach (var group in groups)
                {
                    var scoop = await scoopService.GetByIdAsync(group.ScoopId);
                    var scoopState = await scoopStateService.GetByIdAsync(scoop.StateId);
                    var scoopUsage = await scoopUsageService.GetByScoopIdAsync(scoop.Id);
                    var pots = await potService.GetByGroupIdAsync(group.Id);
                    var potIds = pots.Select(p => p.Id).ToList();

                    var threshold = DateTime.UtcNow.AddHours(-_freshnessHours);

                    var calculated = await calculatedTaskService.GetByPotIdsAsync(potIds);
                    var freshCalculated = calculated.Where(ct => ct.CreatedAt >= threshold).ToList();

                    var analysis = await metalMarkAnalysisService.GetByPotIdsAsync(potIds);
                    var freshAnalysis = analysis.Where(a => a.DateOfReceipt >= threshold && a.MetalMarkId == order.MetalmarkId).ToList();

                    var validPotIds = freshCalculated.Select(c => c.PotId)
                                                     .Intersect(freshAnalysis.Select(a => a.PotId))
                                                     .ToHashSet();
                    var validPots = pots.Where(p => validPotIds.Contains(p.Id)).ToList();

                    if (!validPots.Any())
                    {
                        continue;
                    }

                    foreach (var a in freshAnalysis.Where(x => validPotIds.Contains(x.PotId)))
                    {
                        marksByPot[a.PotId] = a;
                    }

                    foreach (var c in freshCalculated.Where(x => validPotIds.Contains(x.PotId)))
                    {
                        var weight = c.RoundCalculatedTaskForPot ?? c.CalculatedTaskForPot;
                        metalLevelByPot[c.PotId] = weight is double d ? d : (double)c.CalculatedTaskForPot;
                    }

                    var potDtos = await potService.CreateAsync(validPots, freshCalculated, freshAnalysis, marksByPot, metalLevelByPot);
                    var groupDto = potGroupService.Create(group, scoop, scoopState, scoopUsage, potDtos);
                    groupDtos.Add(groupDto);
                }

                if (groupDtos.Any())
                {
                    var buildingInfo = buildingInfoService.CreateViewModel(building, groupDtos, order.MetalmarkId);
                    buildingInfos.Add(buildingInfo);
                }
            }

            if (!buildingInfos.Any())
            {
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                continue;
            }

            ExecutionPlanViewModel plan;
            try
            {
                plan = planSelector.SelectExecutionPlan(buildingInfos, (double)order.RemainingWeight);
            }
            catch (BusinessException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                continue;
            }

            order.Status = 1;
            await orderService.UpdateAsync(order);

            var tasks = await tapTaskReservationService.CreateTasksForOrderAsync(
                order.Id,
                plan,
                marksByPot,
                metalLevelByPot);

            await shiftAssignmentService.AssignTaskAsync(tasks);

            var pouredWeight = plan.Segments.Sum(s => s.MetalWeight);
            order.RemainingWeight -= (decimal)pouredWeight;

            if (order.RemainingWeight <= 0)
            {
                order.Status = 2;
            }

            await orderService.UpdateAsync(order);

            foreach (var segment in plan.Segments)
            {
                await scoopUsageService.CreateAsync(new ScoopUsageDto
                {
                    Id = Guid.NewGuid(),
                    ScoopId = segment.ScoopId,
                    BusyFrom = DateTime.Now,
                    BusyUntil = DateTime.Now.AddHours(1),
                });
            }

            await orderService.SaveChangesAsync();

            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }
}