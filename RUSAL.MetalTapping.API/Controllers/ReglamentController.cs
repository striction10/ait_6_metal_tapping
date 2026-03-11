using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Contracts;
using RUSAL.MetalTapping.BLL.DTOs;
using RUSAL.MetalTapping.BLL.Interfaces;
using RUSAL.MetalTapping.BLL.Services;

namespace RUSAL.MetalTapping.API.Controllers
{
    [ApiController]
    [Route("api/reglament")]
    public class ReglamentController : ControllerBase
    {
        private readonly IGenericService<ReglamentDto> _genericService;
        private readonly DeviationValuesService _deviationValuesService;

        public ReglamentController(
            IGenericService<ReglamentDto> genericService,
            DeviationValuesService deviationValuesService)
        {
            _genericService = genericService;
            _deviationValuesService = deviationValuesService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ReglamentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _genericService.GetAllAsync();
            return Ok(response);
        }

        [HttpGet("table")]
        [ProducesResponseType(typeof(ReglamentTableResponse), StatusCodes.Status200OK)]
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
}