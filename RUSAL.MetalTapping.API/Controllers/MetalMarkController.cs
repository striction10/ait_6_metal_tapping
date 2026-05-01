using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.UseCases;
using RUSAL.MetalTapping.BLL.Application.ViewModels;
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
    [ProducesResponseType(typeof(IEnumerable<MetalMarkViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<MetalMarkViewModel>>> GetAll()
    {
        var response = await _service.ExecuteAsync();
        return Ok(response);
    }
}