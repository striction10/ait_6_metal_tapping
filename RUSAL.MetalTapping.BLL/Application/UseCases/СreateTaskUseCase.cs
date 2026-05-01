using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Application.ViewModels;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;
namespace RUSAL.MetalTapping.BLL.Application.UseCases;

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
    private readonly BuildingService _buildingService = buildingService;
    private readonly PotGroupService _potGroupService = potGroupService;
    private readonly PotService _potService = potService;
    private readonly ScoopService _scoopService = scoopService;
    private readonly ScoopStateService _scoopStateService = scoopStateService;
    private readonly ScoopUsageService _scoopUsageService = scoopUsageService;
    private readonly CalculatedTaskService _calculatedTaskService = calculatedTaskService;
    private readonly MetalMarkAnalysisService _metalMarkAnalysisService = metalMarkAnalysisService;
    private readonly MetalMarkService _metalMarkService = metalMarkService;
    private readonly GroupService _groupService = groupService;
    private readonly BuildingService _buildingInfoService = buildingInfoService;
    private readonly CastingExecutionPlanService _planSelector = planSelector;
    private readonly TapTaskReservationService _tapTaskReservationService = tapTaskReservationService;
    private readonly ShiftAssignmentService _shiftAssignmentService = shiftAssignmentService;

    /// <summary>
    /// Создание ViewModel для отображения таблицы заданий на выливку для смен для заданного корпуса
    /// </summary>
    /// <param name="model"> Данные для отображения таблицы </param>
    /// <returns> ViewModel для отображения таблицы </returns>
    /// <exception cref="BusinessException"> Нет данных для отображения </exception>
    public async Task ExecuteAsync(OrderRequest model)
    {
        var metalMark = await _metalMarkService.GetByNameAsync(model.metalMarkName);

        var buildings = await _buildingService.GetAllAsync();

        var marksByPot = new Dictionary<Guid, MetalMarkAnalysisDto>();
        var metalLevelByPot = new Dictionary<Guid, double>();

        var buildingInfos = new List<BuildingMetalInfoViewModel>();

        foreach (var building in buildings)
        {
            var groups = await _potGroupService.GetByBuildingIdAsync(building.Id);
            var groupDtos = new List<PotGroupViewModel>();

            foreach (var group in groups)
            {
                var scoop = await _scoopService.GetByIdAsync(group.ScoopId);

                var scoopState = await _scoopStateService.GetByIdAsync(scoop.StateId);

                var scoopUsage = await _scoopUsageService.GetByScoopIdAsync(scoop.Id);

                var pots = await _potService.GetByGroupIdAsync(group.Id);
                var potIds = pots.Select(p => p.Id).ToList();

                var calculated = await _calculatedTaskService.GetByPotIdsAsync(potIds);
                if (calculated.Count() != potIds.Count())
                    throw new BusinessException("Missing calculated tasks");

                var analysis = await _metalMarkAnalysisService.GetByPotIdsAsync(potIds);
                if (analysis.Count() != potIds.Count())
                    throw new BusinessException("Missing metal mark analysis");

                var potDtos = await _potService.CreateAsync(
                    pots,
                    calculated,
                    analysis,
                    marksByPot,
                    metalLevelByPot);

                var groupDto = _groupService.Create(
                    group,
                    scoop,
                    scoopState,
                    scoopUsage,
                    potDtos);

                groupDtos.Add(groupDto);
            }

            var buildingInfo = _buildingInfoService.CreateViewModel(building, groupDtos, metalMark.Id);
            buildingInfos.Add(buildingInfo);
        }

        var plan = _planSelector.SelectExecutionPlan(buildingInfos, model.requiredMetalWeight);

        var tasks = await _tapTaskReservationService.CreateTasksAsync(plan, model, metalMark.Id, marksByPot, metalLevelByPot);

        await _shiftAssignmentService.AssignTaskAsync(tasks);
    }
}