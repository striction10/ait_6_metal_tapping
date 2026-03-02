using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.DTOs;
using RUSAL.MetalTapping.BLL.Interfaces;

namespace RUSAL.MetalTapping.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGenericService<UserDto> _service;

        public UserController(IGenericService<UserDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }
    }
}