using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.UseCases;
namespace RUSAL.MetalTapping.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuilidngInfoController(CreateTaskUseCase service) : ControllerBase
{
    private readonly CreateTaskUseCase _service = service;

    /// <summary>
    /// Регистрация запроса и расчёт оптимального маршрута выливки
    /// </summary>
    /// <param name="metalMarkName"> Заказанная марка металла </param>
    /// <param name="requiredMetalWeight"> Заказанное количество металла </param>
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