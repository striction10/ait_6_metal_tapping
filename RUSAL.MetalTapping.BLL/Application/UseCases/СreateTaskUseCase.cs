using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.DTOs;
using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;
namespace RUSAL.MetalTapping.BLL.Application.UseCases;

public class CreateTaskUseCase(
    IGenericRepository<Building> buildingRepository,
    IPotGroupRepository potGroupRepository,
    IPotGroupHistoryRepository potGroupHistoryRepository,
    IGenericRepository<Scoop> scoopRepository,
    IGenericRepository<ScoopState> scoopStateRepository,
    IScoopUsageRepository scoopUsageRepository,
    IGenericRepository<PotState> potStateRepository,
    ICalculatedTaskRepository calculatedTaskRepository,
    IMetalMarkAnalysisRepository metalMarkAnalysisRepository,
    IMetalMarkRepository metalMarkRepository,
    PotService potService,
    GroupService groupService,
    BuildingService buildingInfoService,
    CastingExecutionPlanService planSelector,
    TapTaskService tapTaskService,
    ShiftAssignmentService shiftAssignmentService)
{
    private readonly IGenericRepository<Building> _buildingRepository = buildingRepository;
    private readonly IPotGroupRepository _potGroupRepository = potGroupRepository;
    private readonly IPotGroupHistoryRepository _potGroupHistoryRepository = potGroupHistoryRepository;
    private readonly IGenericRepository<Scoop> _scoopRepository = scoopRepository;
    private readonly IGenericRepository<ScoopState> _scoopStateRepository = scoopStateRepository;
    private readonly IScoopUsageRepository _scoopUsageRepository = scoopUsageRepository;
    private readonly ICalculatedTaskRepository _calculatedTaskRepository = calculatedTaskRepository;
    private readonly IMetalMarkAnalysisRepository _metalMarkAnalysisRepository = metalMarkAnalysisRepository;
    private readonly IMetalMarkRepository _metalMarkRepository = metalMarkRepository;

    private readonly PotService _potService = potService;
    private readonly GroupService _groupService = groupService;
    private readonly BuildingService _buildingInfoService = buildingInfoService;
    private readonly CastingExecutionPlanService _planSelector = planSelector;
    private readonly TapTaskService _tapTaskService = tapTaskService;
    private readonly ShiftAssignmentService _shiftAssignmentService = shiftAssignmentService;

    /// <summary>
    /// Создание ViewModel для отображения таблицы заданий на выливку для смен для заданного корпуса
    /// </summary>
    /// <param name="model"> Данные для отображения таблицы </param>
    /// <returns> ViewModel для отображения таблицы </returns>
    /// <exception cref="BusinessException"> Нет данных для отображения </exception>
    public async Task ExecuteAsync(OrderRequest model)
    {
        var metalMark = EnsureFound(
            await _metalMarkRepository.GetByNameAsync(model.metalMarkName),
            $"Metal mark {model.metalMarkName} not found");

        var buildings = EnsureFound(
            await _buildingRepository.GetAllAsync(),
            "Buildings not found");

        var marksByPot = new Dictionary<Guid, MetalMarkAnalysis>();
        var metalLevelByPot = new Dictionary<Guid, double>();

        var buildingInfos = new List<BuildingMetalInfo>();

        foreach (var building in buildings)
        {
            var groups = await _potGroupRepository.GetByBuildingIdsAsync(building.Id);
            var groupDtos = new List<PotGroupDto>();

            foreach (var group in groups)
            {
                var scoop = EnsureFound(
                    await _scoopRepository.GetByIdAsync(group.ScoopId),
                    $"Scoop {group.ScoopId} not found");

                var scoopState = EnsureFound(
                    await _scoopStateRepository.GetByIdAsync(scoop.StateId),
                    $"Scoop state {scoop.StateId} not found");

                var scoopUsage = await _scoopUsageRepository.GetByScoopIdAsync(scoop.Id);

                var pots = await _potGroupHistoryRepository.GetPotsByGroupIdAsync(group.Id);
                var potIds = pots.Select(p => p.Id).ToList();

                var calculated = await _calculatedTaskRepository.GetByPotIdsAsync(potIds);
                if (calculated.Count() != potIds.Count())
                    throw new BusinessException("Missing calculated tasks");

                var analysis = await _metalMarkAnalysisRepository.GetMetalMarkAnalysisWithPotIdsAsync(potIds);
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

            var buildingInfo = _buildingInfoService.Create(building, groupDtos, metalMark.Id);
            buildingInfos.Add(buildingInfo);
        }

        var plan = _planSelector.SelectExecutionPlan(buildingInfos, model.requiredMetalWeight);

        var tasks = await _tapTaskService.CreateAsync(plan, model, metalMark.Id, marksByPot, metalLevelByPot);

        await _shiftAssignmentService.AssignTaskAsync(tasks);
    }
}