using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.DTOs;
using RUSAL.MetalTapping.BLL.Application.UseCases.Buildings;

namespace RUSAL.MetalTapping.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuildingController : ControllerBase
    {
        private readonly GetAllBuildingsUseCase _useCase;

        public BuildingController(GetAllBuildingsUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<BuildingDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _useCase.ExecuteAsync();
            return Ok(response);
        }
    }
}