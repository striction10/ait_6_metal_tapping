using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.UseCases;

namespace RUSAL.MetalTapping.API.Controllers;

/// <summary>
/// Контроллер заказов.
/// </summary>
/// <param name="useCase"> Оркестратор для работы с распределением заказов на задания на выливку. </param>
[ApiController]
[Route("api/[controller]")]
public class OrderController(CreateTaskUseCase useCase) : ControllerBase
{
    private readonly CreateTaskUseCase useCase = useCase;

    /// <summary>
    /// Регистрация запроса и расчёт оптимального маршрута выливки.
    /// </summary>
    /// <param name="metalMarkName"> Заказанная марка металла. </param>
    /// <param name="requiredMetalWeight"> Заказанное количество металла. </param>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Get(
       [FromQuery] string metalMarkName,
       [FromQuery] double requiredMetalWeight)
    {
        var request = new OrderRequest(metalMarkName, requiredMetalWeight);

        await useCase.ExecuteAsync(request);

        return Ok();
    }
}
