using System.Reflection;
using Microsoft.OpenApi.Models;

namespace RUSAL.MetalTapping.API.Extensions;

/// <summary>
/// Расширение для регистрации и настройки Swagger.
/// </summary>
public static class SwaggerExtension
{
    /// <summary>
    /// Регистрация и настройка Swagger.
    /// </summary>
    /// <param name="services"> Коллекция сервисов. </param>
    public static void AddSwaggerExtension(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "RUSAL.MetalTapping",
                Version = "v1",
                Description = "API for RUSAL.MetalTapping",
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Введите токен в формате: Bearer {your token}",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                },
            });
        });
    }
}
