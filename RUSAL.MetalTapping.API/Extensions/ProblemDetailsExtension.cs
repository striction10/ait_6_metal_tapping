using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;
namespace RUSAL.MetalTapping.API.Extensions;

public static class ProblemDetailsExtension
{
    public static void AddProblemDetailsExtension(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.Map<BusinessException>(ex => new ProblemDetails
            {
                Status = 400,
                Title = "Business error",
                Detail = ex.Message,
                Instance = null
            });

            options.Map<NotFoundException>(ex => new ProblemDetails
            {
                Status = 404,
                Title = "Not found",
                Detail = ex.Message
            });

            options.Map<AlreadyExistsException>(ex => new ProblemDetails
            {
                Status = 409,
                Title = "Conflict",
                Detail = ex.Message
            });

            options.Map<AuthentificationException>(ex => new ProblemDetails
            {
                Status = 401,
                Title = "Unauthorized",
                Detail = ex.Message
            });

            options.Map<Exception>(ex => new ProblemDetails
            {
                Status = 500,
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred"
            });

            options.IncludeExceptionDetails = (ctx, ex) => false;
        });
    }
}