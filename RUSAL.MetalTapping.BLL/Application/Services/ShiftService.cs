using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис для работы со сменами.
/// </summary>
/// <param name="shiftRepository">Репозиторий смен.</param>
/// <param name="mapper">Маппер объектов.</param>
public class ShiftService(
    IShiftRepository shiftRepository,
    IMapper mapper)
{
    /// <summary>
    /// Получение действующих смен.
    /// </summary>
    /// <returns> DTO смен. </returns>
    public async Task<IEnumerable<ShiftDto?>> GetCurrentShifts()
    {
        var entities = EnsureFound(
            await shiftRepository.GetCurrentShifts(),
            $"Current shifts was not found");

        return mapper.Map<IEnumerable<ShiftDto>>(entities);
    }

    /// <summary>
    /// Получение следующих смен после действующих.
    /// </summary>
    /// <returns> DTO смен. </returns>
    public async Task<IEnumerable<ShiftDto?>> GetNextShifts()
    {
        var entities = EnsureFound(
            await shiftRepository.GetNextShifts(),
            $"Next shifts was not found");

        return mapper.Map<IEnumerable<ShiftDto?>>(entities);
    }

    /// <summary>
    /// Получение следующих смен для конкретного корпуса.
    /// </summary>
    /// <param name="buildingId"> Идентификатор электролизёра. </param>
    /// <param name="fromDate"> Дата начала отсчёта. </param>
    /// <returns> DTO смены. </returns>
    public async Task<ShiftDto?> GetNextShiftForBuilding(Guid buildingId, DateTime fromDate)
    {
        var entity = EnsureFound(
            await shiftRepository.GetNextShiftForBuilding(buildingId, fromDate),
            $"Next shift for building {buildingId} was not found");

        return mapper.Map<ShiftDto>(entity);
    }

    /// <summary>
    /// Получение текущей смены для конкретного корпуса.
    /// </summary>
    /// <param name="buildingId"> Идентификатор копруса. </param>
    /// <returns> DTO смены. </returns>
    public async Task<ShiftDto?> GetByBuildingId(Guid buildingId)
    {
        var entity = EnsureFound(
            await shiftRepository.GetByBuildingId(buildingId),
            $"Shift for building {buildingId} was not found");

        return mapper.Map<ShiftDto>(entity);
    }
}
