using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Contracts;
using RUSAL.MetalTapping.BLL.Services;

namespace RUSAL.MetalTapping.API.Controllers
{
    [ApiController]
    [Route("api/reglamentTable")]
    public class ReglamentTableController : ControllerBase
    {
        private readonly DeviationValuesService _service;

        public ReglamentTableController(DeviationValuesService controller)
        {
            _service = controller;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ReglamentTableResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReglamentTableResponse>> GetReglamentTable(
            [FromQuery] Guid buildingId,
            [FromQuery] Guid reglamentId)
        {
            var request = new ReglamentTableRequest(buildingId, reglamentId);
            var response = await _service.GetReglamentTableAsync(request);
            return Ok(response);
        }
    }
}