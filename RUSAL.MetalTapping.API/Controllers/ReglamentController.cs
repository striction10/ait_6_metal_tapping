using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.DTOs;
using RUSAL.MetalTapping.BLL.Interfaces;

namespace RUSAL.MetalTapping.API.Controllers
{
    [ApiController]
    [Route("api/reglament")]
    public class ReglamentController : ControllerBase
    {
        private readonly IGenericService<ReglamentDto> _service;

        public ReglamentController(IGenericService<ReglamentDto> service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ReglamentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllAsync();
            return Ok(response);
        }
    }
}