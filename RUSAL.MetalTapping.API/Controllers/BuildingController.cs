using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.UseCases.Buildings;
using RUSAL.MetalTapping.BLL.Domain.DTOs;

namespace RUSAL.MetalTapping.API.Controllers;

/// <summary>
/// Контроллер корпусов.
/// </summary>
/// <param name="useCase"> Оркестратор работы сервисов с корпусами. </param>
[ApiController]
[Route("api/[controller]")]
public class BuildingController(GetAllBuildingsUseCase useCase) : ControllerBase
{
    /// <summary>
    /// Получение списка всех корпусов.
    /// </summary>
    /// <returns> Список корпусов. </returns>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(IEnumerable<BuildingDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<BuildingDto>>> GetAll()
    {
        var response = await useCase.ExecuteAsync();
        return Ok(response);
    }
}
