using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.Services;

namespace RUSAL.MetalTapping.API.Controllers;

/// <summary>
/// Контроллер отправки файла по почте.
/// </summary>
/// <param name="service"> Сервис отправки файла по почте. </param>
[ApiController]
[Route("api/[controller]")]
public class EmailController(EmailSenderService service) : ControllerBase
{
    /// <summary>
    /// Отправка .pdf на указанный адрес почты.
    /// </summary>
    [HttpPost("send")]
    [Authorize(Roles = "User,Technologist")]
    public async Task<IActionResult> SendPdf(
        [FromForm] string email,
        [FromForm] string subject,
        [FromForm] string message,
        IFormFile pdf)
    {
        if (!pdf.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest("Разрешены только файлы .pdf");
        }

        using var stream = pdf.OpenReadStream();
        var request = new EmailRequest(email, subject, message, stream);
        await service.SendEmailWithPdfBase64Async(request);

        return Ok();
    }
}
