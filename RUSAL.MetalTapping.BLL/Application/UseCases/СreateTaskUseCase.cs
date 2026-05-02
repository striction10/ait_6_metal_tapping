using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Application.ViewModels;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;

namespace RUSAL.MetalTapping.BLL.Application.UseCases;

/// <summary>
/// Оркестратор создания задания на выливку металла.
/// </summary>
/// <param name="buildingService">Сервис для работы с корпусами.</param>
/// <param name="potGroupService">Сервис для работы с группами ковшей.</param>
/// <param name="potService">Сервис для работы с ковшами.</param>
/// <param name="scoopService">Сервис для работы с совками.</param>
/// <param name="scoopStateService">Сервис для работы с состояниями совков.</param>
/// <param name="scoopUsageService">Сервис для работы с использованием совков.</param>
/// <param name="calculatedTaskService">Сервис для работы с рассчитанными заданиями.</param>
/// <param name="metalMarkAnalysisService">Сервис для работы с анализами марок металла.</param>
/// <param name="metalMarkService">Сервис для работы с марками металла.</param>
/// <param name="groupService">Сервис для работы с группами.</param>
/// <param name="buildingInfoService">Сервис для работы с информацией о корпусах.</param>
/// <param name="planSelector">Сервис выбора плана разливки.</param>
/// <param name="tapTaskReservationService">Сервис резервирования заданий на выливку.</param>
/// <param name="shiftAssignmentService">Сервис назначения смен.</param>
public class CreateTaskUseCase(
    BuildingService buildingService,
    PotGroupService potGroupService,
    PotService potService,
    ScoopService scoopService,
    ScoopStateService scoopStateService,
    ScoopUsageService scoopUsageService,
    CalculatedTaskService calculatedTaskService,
    MetalMarkAnalysisService metalMarkAnalysisService,
    MetalMarkService metalMarkService,
    GroupService groupService,
    BuildingService buildingInfoService,
    CastingExecutionPlanService planSelector,
    TapTaskReservationService tapTaskReservationService,
    ShiftAssignmentService shiftAssignmentService)
{
    private readonly BuildingService buildingService = buildingService;
    private readonly PotGroupService potGroupService = potGroupService;
    private readonly PotService potService = potService;
    private readonly ScoopService scoopService = scoopService;
    private readonly ScoopStateService scoopStateService = scoopStateService;
    private readonly ScoopUsageService scoopUsageService = scoopUsageService;
    private readonly CalculatedTaskService calculatedTaskService = calculatedTaskService;
    private readonly MetalMarkAnalysisService metalMarkAnalysisService = metalMarkAnalysisService;
    private readonly MetalMarkService metalMarkService = metalMarkService;
    private readonly GroupService groupService = groupService;
    private readonly BuildingService buildingInfoService = buildingInfoService;
    private readonly CastingExecutionPlanService planSelector = planSelector;
    private readonly TapTaskReservationService tapTaskReservationService = tapTaskReservationService;
    private readonly ShiftAssignmentService shiftAssignmentService = shiftAssignmentService;

    /// <summary>
    /// Создание ViewModel для отображения таблицы заданий на выливку для смен для заданного корпуса.
    /// </summary>
    /// <param name="model"> Данные для отображения таблицы. </param>
    /// <returns> ViewModel для отображения таблицы. </returns>
    /// <exception cref="BusinessException"> Нет данных для отображения. </exception>
    public async Task ExecuteAsync(OrderRequest model)
    {
        var metalMark = await metalMarkService.GetByNameAsync(model.metalMarkName);

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

                var calculated = await calculatedTaskService.GetByPotIdsAsync(potIds);
                if (calculated.Count() != potIds.Count())
                {
                    throw new BusinessException("Missing calculated tasks");
                }

                var analysis = await metalMarkAnalysisService.GetByPotIdsAsync(potIds);
                if (analysis.Count() != potIds.Count())
                {
                    throw new BusinessException("Missing metal mark analysis");
                }

                var potDtos = await potService.CreateAsync(
                    pots,
                    calculated,
                    analysis,
                    marksByPot,
                    metalLevelByPot);

                var groupDto = groupService.Create(
                    group,
                    scoop,
                    scoopState,
                    scoopUsage,
                    potDtos);

                groupDtos.Add(groupDto);
            }

            var buildingInfo = buildingInfoService.CreateViewModel(building, groupDtos, metalMark.Id);
            buildingInfos.Add(buildingInfo);
        }

        var plan = planSelector.SelectExecutionPlan(buildingInfos, model.requiredMetalWeight);

        var tasks = await tapTaskReservationService.CreateTasksAsync(plan, model, metalMark.Id, marksByPot, metalLevelByPot);

        await shiftAssignmentService.AssignTaskAsync(tasks);
    }
}
