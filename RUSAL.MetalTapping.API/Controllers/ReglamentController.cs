using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.UseCases;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.API.Controllers
{
    [ApiController]
    [Route("api/reglament")]
    public class ReglamentController : ControllerBase
    {
        private readonly IGenericService<Reglament> _genericService;
        private readonly DeviationValuesUseCase _deviationValuesService;

        public ReglamentController(
            IGenericService<Reglament> genericService,
            DeviationValuesUseCase deviationValuesService)
        {
            _genericService = genericService;
            _deviationValuesService = deviationValuesService;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(Reglament), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _genericService.GetAllAsync();
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