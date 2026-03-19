using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.DTOs;
using RUSAL.MetalTapping.BLL.Application.UseCases;

namespace RUSAL.MetalTapping.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReglamentController : ControllerBase
    {
        private readonly GetAllReglamentsUseCase _service;
        private readonly DeviationValuesUseCase _deviationValuesService;

        public ReglamentController(
            GetAllReglamentsUseCase service,
            DeviationValuesUseCase deviationValuesService)
        {
            _service = service;
            _deviationValuesService = deviationValuesService;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<ReglamentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.ExecuteAsync();
            return Ok(response);
        }

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
}