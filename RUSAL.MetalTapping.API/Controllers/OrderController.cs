using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.Services;
namespace RUSAL.MetalTapping.API.Controllers;

/// <summary>
/// Контроллер заказов.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrderController(
    OrderService orderService,
    MetalMarkService metalMarkService) : ControllerBase
{
    /// <summary>
    /// Создание заказа на выливку (добавление в очередь).
    /// </summary>
    /// <param name="metalMarkName"> Заказанная марка металла. </param>
    /// <param name="requiredMetalWeight"> Заказанное количество металла. </param>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create(
       [FromQuery] string metalMarkName,
       [FromQuery] double requiredMetalWeight)
    {
        var metalMark = await metalMarkService.GetByNameAsync(metalMarkName);

        var order = await orderService.AcceptOrderAsync(
            requiredMetalWeight,
            metalMark.Id);

        return Accepted(new
        {
            orderId = order.Id,
            message = "Заказ принят в очередь на выливку",
            remainingWeight = order.RemainingWeight,
            status = "Pending"
        });
    }
}