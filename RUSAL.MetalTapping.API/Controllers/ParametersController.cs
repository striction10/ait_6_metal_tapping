using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.UseCases;

namespace RUSAL.MetalTapping.API.Controllers
{
    [ApiController]
    [Route("api/parameters")]
    public class ParametersController : ControllerBase
    {
        private readonly ProcessDeviationAndTaskUseCase _processDeviationAndTask;
        private readonly ProcessCalculatedTaskUseCase _processCalculatedTask;
        private readonly ViewDeviationAndTaskUseCase _viewDeviationAndTask;

        public ParametersController(
            ProcessDeviationAndTaskUseCase processDeviationAndTask,
            ProcessCalculatedTaskUseCase processCalculatedTask,
            ViewDeviationAndTaskUseCase viewDeviationAndTask)
        {
            _processDeviationAndTask = processDeviationAndTask;
            _processCalculatedTask = processCalculatedTask;
            _viewDeviationAndTask = viewDeviationAndTask;
        }

        [HttpPost]
        [Authorize(Roles = "Operator,Technologist")]
        [ProducesResponseType(typeof(ProcessDeviationAndTaskResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<ProcessDeviationAndTaskResponse>> PutParameters(
            [FromQuery] Guid potId,
            [FromQuery] double actualMetalLevel)
        {
            var request = new ProcessDeviationAndTaskRequest(potId, actualMetalLevel);
            var response = await _processDeviationAndTask.ExecuteAsync(request);
            return Ok(response);
        }

        [HttpGet("table")]
        [Authorize(Roles = "Operator,Technologist")]
        [ProducesResponseType(typeof(ViewDeviationAndTaskResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<ViewDeviationAndTaskResponse>> GetTable(
            [FromQuery] Guid reglamentId,
            [FromQuery] Guid buidlingId)
        {
            var request = new ViewDeviationAndTaskRequest(reglamentId, buidlingId);
            var response = await _viewDeviationAndTask.ExecuteAsync(request);
            return Ok(response);
        }

        [HttpPost("calculated")]
        [Authorize(Roles = "Technologist")]
        [ProducesResponseType(typeof(ProcessCalculatedTaskResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<ProcessCalculatedTaskResponse>> PutCalculatedTask(
            [FromQuery] Guid potId,
            [FromQuery] double calculatedTask,
            [FromQuery] double? roundCalculatedTask)
        {
            var request = new ProcessCalculatedTaskRequest(potId, calculatedTask, roundCalculatedTask);
            var response = await _processCalculatedTask.ExecuteAsync(request);
            return Ok(response);
        }
    }
}