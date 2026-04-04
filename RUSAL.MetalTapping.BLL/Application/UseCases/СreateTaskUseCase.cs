using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.UseCases
{
    public class CreateTaskUseCase
    {
        private readonly IGenericRepository<Building> _buildingRepository;
        private readonly IGenericRepository<Scoop> _scoopRepository;
        private readonly IGenericRepository<ScoopState> _scoopStateRepository;
        private readonly IMetalMarkRepository _metalMarkRepository;
        private readonly IPotGroupRepository _potGroupRepository;
        private readonly IPotGroupHistoryRepository _potGroupHistoryRepository;
        private readonly IScoopUsageRepository _scoopUsageRepository;
        private readonly ICalculatedTaskRepository _calculatedTaskRepository;
        private readonly IMetalMarkAnalysisRepository _metalMarkAnalysisRepository;

        public CreateTaskUseCase(
            IGenericRepository<Building> buildingRepository,
            IGenericRepository<Scoop> scoopRepository,
            IGenericRepository<ScoopState> scoopStateRepository,
            IMetalMarkRepository metalMarkRepository,
            IPotGroupRepository potGroupRepository,
            IPotGroupHistoryRepository potGroupHistoryRepository,
            IScoopUsageRepository scoopUsageRepository,
            ICalculatedTaskRepository calculatedTaskRepository,
            IMetalMarkAnalysisRepository metalMarkAnalysisRepository)
        {
            _buildingRepository = buildingRepository;
            _scoopRepository = scoopRepository;
            _scoopStateRepository = scoopStateRepository;
            _metalMarkRepository = metalMarkRepository;
            _potGroupRepository = potGroupRepository;
            _potGroupHistoryRepository = potGroupHistoryRepository;
            _scoopUsageRepository = scoopUsageRepository;
            _calculatedTaskRepository = calculatedTaskRepository;
            _metalMarkAnalysisRepository = metalMarkAnalysisRepository;
        }

        public async Task ExecuteAsync(OrderRequest model)
        {
            var buildings = EnsureFound(
                await _buildingRepository.GetAllAsync(),
                $"Builidngs not found");

            var metalMark = EnsureFound(
                await _metalMarkRepository.GetByNameAsync(model.metalMarkName),
                $"Metal mark with name {model.metalMarkName} not found");

            var buildingInfos = new List<BuildingMetalInfo>();

            foreach (var building in buildings)
            {
                var groups = await _potGroupRepository.GetByBuildingIdAsync(building.Id);

                var groupDtos = new List<PotGroupDto>();

                foreach (var group in groups)
                {
                    var scoop = EnsureFound(
                        await _scoopRepository.GetByIdAsync(group.ScoopId),
                        $"Scoop with id {group.ScoopId} not found");

                    var scoopState = EnsureFound(
                        await _scoopStateRepository.GetByIdAsync(scoop.StateId),
                        $"Scoop state with id {scoop.StateId} not found");

                    var scoopUsages = await _scoopUsageRepository.GetByScoopIdAsync(scoop.Id);

                    var isBusy = scoopUsages?.Any(u => u.BusyUntil > DateTime.UtcNow) ?? false;

                    var scoopDto = new ScoopDto
                    {
                        Id = scoop.Id,
                        State = scoopState.Name,
                        IsBusy = isBusy
                    };

                    var pots = await _potGroupHistoryRepository.GetPotsByGroupIdAsync(group.Id);

                    var potIds = pots.Select(p => p.Id).ToList();

                    var calculatedTasks = await _calculatedTaskRepository.GetByPotIdsAsync(potIds);

                    if (calculatedTasks.Count() != potIds.Count())
                    {
                        var missing = potIds.Except(calculatedTasks.Select(ct => ct.PotId));
                        var missingNames = pots.Where(p => missing.Contains(p.Id)).Select(p => p.Name);

                        throw new BusinessException(
                            $"Calculated tasks missing for pots: {string.Join(", ", missingNames)}");
                    }

                    var calculatedByPot = calculatedTasks.ToDictionary(ct => ct.PotId);

                    var metalMarks = await _metalMarkAnalysisRepository.GetMetalMarkAnalysisWithPotIdsAsync(potIds);

                    if (metalMarks.Count() != potIds.Count())
                    {
                        var missing = potIds.Except(metalMarks.Select(ma => ma.PotId));
                        var missingNames = pots.Where(p => missing.Contains(p.Id)).Select(p => p.Name);

                        throw new BusinessException(
                            $"MetalMarks missing for pots: {string.Join(", ", missingNames)}");
                    }

                    var marksByPot = metalMarks.ToDictionary(ct => ct.PotId);

                    var potDtos = pots.Select(p =>
                    {
                        var calc = calculatedByPot[p.Id];
                        var marks = marksByPot[p.Id];

                        return new PotDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            MetalLevel = calc.RoundCalculatedTaskForPot 
                            ?? throw new BusinessException($"CalculatedMetalLevel is null for pot {p.Name}"),
                            MetalMarkId = marks.MetalMarkId
                        };
                    }).ToList();

                    var groupDto = new PotGroupDto
                    {
                        Id = group.Id,
                        Scoop = scoopDto,
                        Pots = potDtos
                    };

                    groupDtos.Add(groupDto);
                }
            }
        }
    }
}