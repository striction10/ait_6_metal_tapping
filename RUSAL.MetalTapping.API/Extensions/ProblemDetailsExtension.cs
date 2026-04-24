using RUSAL.MetalTapping.BLL.Domain.Exceptions;
namespace RUSAL.MetalTapping.API.Extensions;

public static class ProblemDetailsExtension
{
    public static void AddProblemDetailsExtension(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                var ex = context.Exception;

                if (ex is null) return;

                context.ProblemDetails.Instance = context.HttpContext.Request.Path;

                context.ProblemDetails.Status = ex switch
                {
                    BusinessException => 400,
                    NotFoundException => 404,
                    AlreadyExistsException => 409,
                    AuthentificationException => 401,
                    _ => 500
                };

                context.ProblemDetails.Title = context.ProblemDetails.Status switch
                {
                    400 => "Business error",
                    404 => "Not found",
                    409 => "Conflict",
                    401 => "Unauthorized",
                    _ => "Internal Server Error"
                };

                context.ProblemDetails.Detail =
                    context.ProblemDetails.Status == 500
                        ? "Unexpected error"
                        : ex.Message;
            };
        });
    }
}