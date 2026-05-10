using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Application.UseCases.Buildings;
using RUSAL.MetalTapping.BLL.Application.ViewModels;
using RUSAL.MetalTapping.BLL.Domain.DTOs;

namespace RUSAL.MetalTapping.API.Controllers;

/// <summary>
/// Контроллер корпусов.
/// </summary>
/// <param name="useCase"> Оркестратор работы сервисов с корпусами. </param>
/// <param name="buildingInfoService"> Сервис получения информации для карты корпуса. </param>
[ApiController]
[Route("api/[controller]")]
public class BuildingController(
    GetAllBuildingsUseCase useCase,
    BuildingInfoService buildingInfoService) : ControllerBase
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

    /// <summary>
    /// Получение информации о корпусе для карты корпуса.
    /// </summary>
    /// <param name="builidngId">Идентификатор корпуса.</param>
    /// <returns>ViewModel корпуса.</returns>
    [HttpGet("map")]
    [Authorize(Roles = "User, Technologist")]
    [ProducesResponseType(typeof(IEnumerable<BuildingViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<BuildingViewModel>> GetMap(Guid builidngId)
    {
        var response = await buildingInfoService.GetBuilidngInfo(builidngId);

        return Ok(response);
    }
}
