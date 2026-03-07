using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.DTOs;
using RUSAL.MetalTapping.BLL.Interfaces;

namespace RUSAL.MetalTapping.API.Controllers
{
    [ApiController]
    [Route("api/building")]
    public class BuildingController : ControllerBase
    {
        private readonly IGenericService<BuildingDto> _service;

        public BuildingController(IGenericService<BuildingDto> service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(BuildingDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllAsync();
            return Ok(response);
        }
    }
}