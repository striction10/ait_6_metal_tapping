using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using RUSAL.MetalTapping.BLL.Application.Contracts;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис отправки сообщений на адрес почты.
/// </summary>
/// <param name="configuration">Конфигурация приложения.</param>
public class EmailSenderService(IConfiguration configuration)
{
    /// <summary>
    /// Отправка сообщений на указанный адрес почты.
    /// </summary>
    /// <param name="request"> Запрос на отправку сообщения на указанный адрес почты.</param>
    public async Task SendEmailWithPdfBase64Async(EmailRequest request)
    {
        var settings = configuration.GetSection("EmailSettings");

        using var client = new SmtpClient();
        await client.ConnectAsync(
            settings["SmtpHost"],
            int.Parse(settings["SmtpPort"]),
            SecureSocketOptions.SslOnConnect);

        await client.AuthenticateAsync(settings["SenderEmail"], settings["SenderPassword"]);

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Выливка металла", settings["SenderEmail"]));
        message.To.Add(new MailboxAddress("Получатель", request.email));
        message.Subject = request.subject;

        var bodyBuilder = new BodyBuilder { TextBody = request.message };
        if (request.pdf != null && request.pdf.CanRead)
        {
            bodyBuilder.Attachments.Add("report.pdf", request.pdf);
        }

        message.Body = bodyBuilder.ToMessageBody();

        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
