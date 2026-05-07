using RUSAL.MetalTapping.API.Extensions;
using RUSAL.MetalTapping.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSwaggerExtension();

builder.Services.AddProblemDetailsExtension();

builder.Services.AddAuthenticationExtension(builder.Configuration);

builder.Services.AddDAL(builder.Configuration);

builder.Services.AddBLL();

var app = builder.Build();

app.UseCors("AllowFrontend");

app.UseProblemDetailsExtension();

app.UseAppPipeline();

app.Run();
