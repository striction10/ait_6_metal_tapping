using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.UseCases;
using RUSAL.MetalTapping.BLL.Application.ViewModels;

namespace RUSAL.MetalTapping.API.Controllers;

/// <summary>
/// Контроллер марок металла.
/// </summary>
/// <param name="useCase"> Оркестратор для работы сервисов с марками металла. </param>
[ApiController]
[Route("api/[controller]")]
public class MetalMarkController(GetAllMetalMarksUseCase useCase) : ControllerBase
{
    private readonly GetAllMetalMarksUseCase useCase = useCase;

    /// <summary>
    /// Получение списка всех марок металла.
    /// </summary>
    /// <returns> Список марок металла. </returns>
    [HttpGet("all")]
    [Authorize]
    [ProducesResponseType(typeof(IEnumerable<MetalMarkViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<MetalMarkViewModel>>> GetAll()
    {
        var response = await useCase.ExecuteAsync();
        return Ok(response);
    }
}
