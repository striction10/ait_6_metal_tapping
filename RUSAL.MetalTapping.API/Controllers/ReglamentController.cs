using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.UseCases;
using RUSAL.MetalTapping.BLL.Application.ViewModels;

namespace RUSAL.MetalTapping.API.Controllers;

/// <summary>
/// Контроллер для работы с регламентами на выливку.
/// </summary>
/// <param name="getAllUseCase"> Оркестратор для работы сервисов по отображению регламентов. </param>
/// <param name="deviationValuesUseCase"> Оркестратор для работы сервисов по расчёту отклонений. </param>
[ApiController]
[Route("api/[controller]")]
public class ReglamentController(
    GetAllReglamentsUseCase getAllUseCase,
    DeviationValuesUseCase deviationValuesUseCase) : ControllerBase
{
    /// <summary>
    /// Получение списка регламентов.
    /// </summary>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(IEnumerable<ReglamentViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<ReglamentViewModel>>> GetAll()
    {
        var response = await getAllUseCase.ExecuteAsync();
        return Ok(response);
    }

    /// <summary>
    /// Получение списка регламентных отклонений электролизёров для заданного коропуса.
    /// </summary>
    /// <param name="buildingId"> Идентификатор электролизёра. </param>
    /// <param name="reglamentId"> Значение расчётного задания. </param>
    [HttpGet("table")]
    [Authorize]
    [ProducesResponseType(typeof(ReglamentTableResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReglamentTableResponse>> GetReglamentTable(
        [FromQuery] Guid buildingId,
        [FromQuery] Guid reglamentId)
    {
        var request = new ReglamentTableRequest(buildingId, reglamentId);
        var response = await deviationValuesUseCase.GetReglamentTableAsync(request);
        return Ok(response);
    }
}
