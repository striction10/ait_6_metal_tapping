using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RUSAL.MetalTapping.BLL.Domain.Auth;
using System.Text;

namespace RUSAL.MetalTapping.API.Extensions;

public static class AuthenticationExtension
{
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
                    Encoding.UTF8.GetBytes(configuration["JwtOptions:SecretKey"]))
            };

            options.Events = new JwtBearerEvents
            {
                OnChallenge = async context =>
                {
                    context.HandleResponse();

                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";

                    var problem = new ProblemDetails
                    {
                        Status = 401,
                        Title = "Unauthorized",
                        Detail = "You are not authorized",
                        Instance = context.HttpContext.Request.Path
                    };

                    await context.Response.WriteAsJsonAsync(problem);
                },

                OnForbidden = async context =>
                {
                    context.Response.StatusCode = 403;

                    var problem = new ProblemDetails
                    {
                        Status = 403,
                        Title = "Forbidden",
                        Detail = "You don't have access",
                        Instance = context.HttpContext.Request.Path
                    };

                    await context.Response.WriteAsJsonAsync(problem);
                }
            };
        });
    }
}