using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.UseCases;

namespace RUSAL.MetalTapping.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParametersController : ControllerBase
    {
        private readonly ProcessDeviationAndTaskUseCase _processDeviationAndTask;
        private readonly ProcessCalculatedTaskUseCase _processCalculatedTask;
        private readonly ViewDeviationAndTaskUseCase _viewDeviationAndTask;
        private readonly ProcessRoundTaskUseCase _processRoundTask;

        public ParametersController(
            ProcessDeviationAndTaskUseCase processDeviationAndTask,
            ProcessCalculatedTaskUseCase processCalculatedTask,
            ViewDeviationAndTaskUseCase viewDeviationAndTask,
            ProcessRoundTaskUseCase processRoundTask)
        {
            _processDeviationAndTask = processDeviationAndTask;
            _processCalculatedTask = processCalculatedTask;
            _viewDeviationAndTask = viewDeviationAndTask;
            _processRoundTask = processRoundTask;
        }

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
}