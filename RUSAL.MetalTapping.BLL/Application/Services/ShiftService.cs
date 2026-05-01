using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class ShiftService(
    IShiftRepository shiftRepository,
    IMapper mapper)
{
    private readonly IShiftRepository _shiftRepository = shiftRepository;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Получение действующих смен
    /// </summary>
    public async Task<IEnumerable<ShiftDto?>> GetCurrentShifts()
    {
        var entities = EnsureFound(await _shiftRepository.GetCurrentShifts(),
            $"Current shifts was not found");

        return _mapper.Map<IEnumerable<ShiftDto>>(entities);
    }

    /// <summary>
    /// Получение следующих смен после действующих
    /// </summary>
    public async Task<IEnumerable<ShiftDto?>> GetNextShifts()
    {
        var entities = EnsureFound(await _shiftRepository.GetNextShifts(),
            $"Next shifts was not found");

        return _mapper.Map<IEnumerable<ShiftDto?>>(entities);
    }

    /// <summary>
    /// Получение следующих смен для конкретного корпуса
    /// </summary>
    /// <param name="buildingId"> Идентификатор электролизёра </param>
    /// <param name="fromDate"> Дата начала отсчёта </param>
    /// <returns> DTO смены </returns>
    public async Task<ShiftDto?> GetNextShiftForBuilding(Guid buildingId, DateTime fromDate)
    {
        var entity = EnsureFound(await _shiftRepository.GetNextShiftForBuilding(buildingId, fromDate),
            $"Next shift for building {buildingId} was not found");

        return _mapper.Map<ShiftDto>(entity);
    }

    /// <summary>
    /// Получение текущей смены для конкретного корпуса
    /// </summary>
    /// <param name="buildingId"></param>
    /// <returns> DTO смены </returns>
    public async Task<ShiftDto?> GetByBuildingId(Guid buildingId)
    {
        var entity = EnsureFound(await _shiftRepository.GetByBuildingId(buildingId),
            $"Shift for building {buildingId} was not found");

        return _mapper.Map<ShiftDto>(entity);
    }
}