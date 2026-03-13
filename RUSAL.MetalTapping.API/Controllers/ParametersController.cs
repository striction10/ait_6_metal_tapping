using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Contracts;
using RUSAL.MetalTapping.BLL.Services;

namespace RUSAL.MetalTapping.API.Controllers
{
    [ApiController]
    [Route("api/parameters")]
    public class ParametersController : ControllerBase
    {
        private readonly PotParametersService _service;

        public ParametersController(PotParametersService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProcessDeviationAndTaskResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProcessDeviationAndTaskResponse>> PutParameters(
            [FromQuery] Guid potId,
            [FromQuery] double actualMetalLevel)
        {
            var request = new ProcessDeviationAndTaskRequest(potId, actualMetalLevel);
            var response = await _service.ProcessDeviationAndTaskAsync(request);
            return Ok(response);
        }

        [HttpGet("table")]
        [ProducesResponseType(typeof(ViewDeviationAndTaskResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ViewDeviationAndTaskResponse>> GetTable(
            [FromQuery] Guid reglamentId,
            [FromQuery] Guid buidlingId)
        {
            var request = new ViewDeviationAndTaskRequest(reglamentId, buidlingId);
            var response = await _service.ViewDeviationAndTaskAsync(request);
            return Ok(response);
        }

        [HttpPost("calculated")]
        [ProducesResponseType(typeof(ProcessCalculatedTaskResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProcessCalculatedTaskResponse>> PutCalculatedTask(
            [FromQuery] Guid potId,
            [FromQuery] double calculatedTask,
            [FromQuery] double? roundCalculatedTask)
        {
            var request = new ProcessCalculatedTaskRequest(potId, calculatedTask, roundCalculatedTask);
            var response = await _service.ProcessCalculatedTaskAsync(request);
            return Ok(response);
        }
    }
}