using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RUSAL.MetalTapping.BLL.Domain.Auth;

namespace RUSAL.MetalTapping.API.Extensions;

/// <summary>
/// Расширение для регистрации и настройки аутентификации.
/// </summary>
public static class AuthenticationExtension
{
    /// <summary>
    /// Регистрация и настройка аутентификации. 
    /// </summary>
    /// <param name="services"> Коллекция сервисов. </param>
    /// <param name="configuration"> Конфигурация. </param>
    public static void AddAuthenticationExtension(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(
            configuration.GetSection("JwtOptions"));

        services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = configuration["JwtOptions:Issuer"],
                ValidAudience = configuration["JwtOptions:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JwtOptions:SecretKey"])),
            };

            options.Events = new JwtBearerEvents
            {
                OnChallenge = async context =>
                {
                    context.HandleResponse();

                    context.Response.StatusCode = ((int)HttpStatusCode.Unauthorized);
                    context.Response.ContentType = "application/json";

                    var problem = new ProblemDetails
                    {
                        Status = ((int)HttpStatusCode.Unauthorized),
                        Title = "Unauthorized",
                        Detail = "You are not authorized",
                        Instance = context.HttpContext.Request.Path,
                    };

                    await context.Response.WriteAsJsonAsync(problem);
                },

                OnForbidden = async context =>
                {
                    context.Response.StatusCode = ((int)HttpStatusCode.Forbidden);

                    var problem = new ProblemDetails
                    {
                        Status = ((int)HttpStatusCode.Forbidden),
                        Title = "Forbidden",
                        Detail = "You don't have access",
                        Instance = context.HttpContext.Request.Path
                    };

                    await context.Response.WriteAsJsonAsync(problem);
                },
            };
        });
    }
}
