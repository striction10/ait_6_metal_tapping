using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Contracts;
using RUSAL.MetalTapping.BLL.DTOs;
using RUSAL.MetalTapping.BLL.Interfaces;

namespace RUSAL.MetalTapping.API.Controllers
{
    [ApiController]
    [Route("api/metalMark")]
    public class MetalMarkController : ControllerBase
    {
        private readonly IGenericService<MetalMarkDto> _service;

        public MetalMarkController(IGenericService<MetalMarkDto> service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(MetalMarkDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllAsync();
            return Ok(response);
        }
    }
}