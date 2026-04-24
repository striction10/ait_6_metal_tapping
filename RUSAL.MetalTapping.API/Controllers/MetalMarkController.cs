using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.DTOs;
using RUSAL.MetalTapping.BLL.Application.UseCases;
namespace RUSAL.MetalTapping.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MetalMarkController(GetAllMetalMarksUseCase service) : ControllerBase
{
    private readonly GetAllMetalMarksUseCase _service = service;

    /// <summary>
    /// Получение списка всех марок металла
    /// </summary>
    /// <returns> Список марок металла </returns>
    [HttpGet("all")]
    [Authorize]
    [ProducesResponseType(typeof(IEnumerable<MetalMarkDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<MetalMarkDto>>> GetAll()
    {
        var response = await _service.ExecuteAsync();
        return Ok(response);
    }
}