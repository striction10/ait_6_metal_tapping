using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.UseCases;
using RUSAL.MetalTapping.BLL.Application.ViewModels;
namespace RUSAL.MetalTapping.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReglamentController(
    GetAllReglamentsUseCase service,
    DeviationValuesUseCase deviationValuesService) : ControllerBase
{
    private readonly GetAllReglamentsUseCase _service = service;
    private readonly DeviationValuesUseCase _deviationValuesService = deviationValuesService;

    /// <summary>
    /// Получение списка регламентов
    /// </summary>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(IEnumerable<ReglamentViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<ReglamentViewModel>>> GetAll()
    {
        var response = await _service.ExecuteAsync();
        return Ok(response);
    }

    /// <summary>
    /// Получение списка регламентных отклонений электролизёров для заданного коропуса
    /// </summary>
    /// <param name="buildingId"> Идентификатор электролизёра </param>
    /// <param name="reglamentId"> Значение расчётного задания </param>
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
        var response = await _deviationValuesService.GetReglamentTableAsync(request);
        return Ok(response);
    }
}