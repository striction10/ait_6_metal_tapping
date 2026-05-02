using RUSAL.MetalTapping.API.Extensions;
using RUSAL.MetalTapping.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSwaggerExtension();

builder.Services.AddProblemDetailsExtension();

builder.Services.AddAuthenticationExtension(builder.Configuration);

builder.Services.AddDAL(builder.Configuration);

builder.Services.AddBLL();

var app = builder.Build();

app.UseProblemDetailsExtension();

app.UseAppPipeline();

app.Run();
