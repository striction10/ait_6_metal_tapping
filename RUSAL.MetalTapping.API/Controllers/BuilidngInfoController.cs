using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.UseCases;

namespace RUSAL.MetalTapping.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuilidngInfoController : ControllerBase
    {
        private readonly CreateTaskUseCase _service;

        public BuilidngInfoController(CreateTaskUseCase service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get(
           [FromQuery] string metalMarkName,
           [FromQuery] double requiredMetalWeight)
        {
            var request = new OrderRequest(metalMarkName, requiredMetalWeight);

            await _service.ExecuteAsync(request);

            return Ok();
        }
    }
}