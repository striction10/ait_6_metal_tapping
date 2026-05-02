using System.Net;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;

namespace RUSAL.MetalTapping.API.Extensions;

/// <summary>
/// Расширение для настройки ProblemDetails middleware.
/// </summary>
public static class ProblemDetailsExtension
{
    /// <summary>
    /// Регистрация сервисов ProblemDetails с маппингом исключений на HTTP статус-коды.
    /// </summary>
    /// <param name="services"> Коллекция сервисов. </param>
    public static void AddProblemDetailsExtension(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.Map<BusinessException>(ex => new ProblemDetails
            {
                Status = (int)(HttpStatusCode.BadRequest),
                Title = "Business error",
                Detail = ex.Message,
                Instance = null,
            });

            options.Map<NotFoundException>(ex => new ProblemDetails
            {
                Status = (int)(HttpStatusCode.NotFound),
                Title = "Not found",
                Detail = ex.Message,
            });

            options.Map<AlreadyExistsException>(ex => new ProblemDetails
            {
                Status = (int)(HttpStatusCode.Conflict),
                Title = "Conflict",
                Detail = ex.Message,
            });

            options.Map<AuthentificationException>(ex => new ProblemDetails
            {
                Status = (int)(HttpStatusCode.Unauthorized),
                Title = "Unauthorized",
                Detail = ex.Message,
            });

            options.Map<Exception>(ex => new ProblemDetails
            {
                Status = (int)(HttpStatusCode.InternalServerError),
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred",
            });

            options.IncludeExceptionDetails = (ctx, ex) => false;
        });
    }

    /// <summary>
    /// Добавляет ProblemDetails middleware и обработку страниц статус-кодов в pipeline.
    /// </summary>
    /// <param name="app"> Application Builder. </param>
    public static void UseProblemDetailsExtension(this IApplicationBuilder app)
    {
        app.UseProblemDetails();
        app.UseStatusCodePages();
    }
}
