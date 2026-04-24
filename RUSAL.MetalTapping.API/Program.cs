using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.API.Extensions;
using RUSAL.MetalTapping.BLL.Domain.Auth;
using RUSAL.MetalTapping.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
}

builder.Services.AddSwaggerExtension();

builder.Services.AddProblemDetailsExtension();

builder.Services.AddAuthenticationExtension(builder.Configuration);

builder.Services.AddDAL(connectionString);

builder.Services.AddBLL();

var app = builder.Build();

app.UseAppPipeline();

app.Run();