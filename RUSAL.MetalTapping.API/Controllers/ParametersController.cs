using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.UseCases;
namespace RUSAL.MetalTapping.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParametersController(
    ProcessDeviationAndTaskUseCase processDeviationAndTask,
    ProcessCalculatedTaskUseCase processCalculatedTask,
    ViewDeviationAndTaskUseCase viewDeviationAndTask,
    ProcessRoundTaskUseCase processRoundTask) : ControllerBase
{
    private readonly ProcessDeviationAndTaskUseCase _processDeviationAndTask = processDeviationAndTask;
    private readonly ProcessCalculatedTaskUseCase _processCalculatedTask = processCalculatedTask;
    private readonly ViewDeviationAndTaskUseCase _viewDeviationAndTask = viewDeviationAndTask;
    private readonly ProcessRoundTaskUseCase _processRoundTask = processRoundTask;

    /// <summary>
    /// Запись актуального уровня металла внутри электролизёра
    /// </summary>
    /// <param name="potId"> Идентификатор электролизёра </param>
    /// <param name="actualMetalLevel"> Актуальный уровень металла </param>
    [HttpPost]
    [Authorize(Roles = "User,Technologist")]
    [ProducesResponseType(typeof(ProcessDeviationAndTaskResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProcessDeviationAndTaskResponse>> PutParameters(
        [FromQuery] Guid potId,
        [FromQuery] double actualMetalLevel)
    {
        var request = new ProcessDeviationAndTaskRequest(potId, actualMetalLevel);
        var response = await _processDeviationAndTask.ExecuteAsync(request);
        return Ok(response);
    }

    /// <summary>
    /// Получение списка параметров электролизёра в заданном корпусе
    /// </summary>
    /// <param name="reglamentId"> Идентификатор регламента </param>
    /// <param name="buidlingId"> Идентификатор корпуса </param>
    [HttpGet("table")]
    [Authorize(Roles = "User,Technologist")]
    [ProducesResponseType(typeof(ViewDeviationAndTaskResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ViewDeviationAndTaskResponse>> GetTable(
        [FromQuery] Guid reglamentId,
        [FromQuery] Guid buidlingId)
    {
        var request = new ViewDeviationAndTaskRequest(reglamentId, buidlingId);
        var response = await _viewDeviationAndTask.ExecuteAsync(request);
        return Ok(response);
    }

    /// <summary>
    /// Запись расчётного задания при нерегламентном отклонении
    /// </summary>
    /// <param name="potId"> Идентификатор электролизёра </param>
    /// <param name="calculatedTask"> Значение расчётного задания </param>
    [HttpPost("calculated/{potId}")]
    [Authorize(Roles = "Technologist")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutCalculatedTask(
        [FromRoute] Guid potId,
        [FromQuery] double calculatedTask)
    {
        var request = new ProcessCalculatedTaskRequest(potId, calculatedTask);
        await _processCalculatedTask.ExecuteAsync(request);
        return Ok();
    }

    /// <summary>
    /// Запись расчётного задания при нерегламентном отклонении
    /// </summary>
    /// <param name="potId"> Идентификатор электролизёра </param>
    /// <param name="roundTask"> Значение расчётного задания </param>
    [HttpPost("round/{potId}")]
    [Authorize(Roles = "Technologist")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutRoundTask(
        [FromRoute] Guid potId,
        [FromQuery] double roundTask)
    {
        var request = new ProcessRoundTaskRequest(potId, roundTask);
        await _processRoundTask.ExecuteAsync(request);
        return Ok();
    }
}