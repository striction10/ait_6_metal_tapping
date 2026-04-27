using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.UseCases.Buildings;
using RUSAL.MetalTapping.BLL.Application.ViewModels;

namespace RUSAL.MetalTapping.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuildingController(GetAllBuildingsUseCase useCase) : ControllerBase
{
    private readonly GetAllBuildingsUseCase _useCase = useCase;

    /// <summary>
    /// Получение списка всех корпусов
    /// </summary>
    /// <returns> Список корпусов </returns>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(IEnumerable<BuildingDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<BuildingDto>>> GetAll()
    {
        var response = await _useCase.ExecuteAsync();
        return Ok(response);
    }
}