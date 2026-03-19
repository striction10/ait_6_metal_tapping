using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.DTOs;
using RUSAL.MetalTapping.BLL.Application.UseCases;

namespace RUSAL.MetalTapping.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetalMarkController : ControllerBase
    {
        private readonly GetAllMetalMarksUseCase _service;

        public MetalMarkController(GetAllMetalMarksUseCase service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<BuildingDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.ExecuteAsync();
            return Ok(response);
        }
    }
}