using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.DTOs;
using RUSAL.MetalTapping.BLL.Application.Services;
namespace RUSAL.MetalTapping.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController(ViewTaskService service) : ControllerBase
{
    private readonly ViewTaskService _service = service;

    /// <summary>
    /// Получение списка заданий на указанную дату
    /// </summary>
    /// <param name="taskRequest"> Целевая дата и идентификатор корпуса, для которого формируется список заданий на выливку</param>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(DailyTaskResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<DailyTaskResponse>> GetDaily([FromBody]TaskRequest taskRequest)
    {
        var response = await _service.ViewTask(taskRequest);

        return Ok(response); 
    }
}