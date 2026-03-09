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
    }
}
