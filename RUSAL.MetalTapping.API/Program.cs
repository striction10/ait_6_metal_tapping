using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.API.Middleware;
using RUSAL.MetalTapping.BLL.DTOs;
using RUSAL.MetalTapping.BLL.Interfaces;
using RUSAL.MetalTapping.BLL.Services;
using RUSAL.MetalTapping.DAL.Auth;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Interfaces;
using RUSAL.MetalTapping.DAL.Models;
using RUSAL.MetalTapping.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "RUSAL.MetalTapping",
        Version = "v1",
        Description = "API for RUSAL.MetalTapping"
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IReglamentRepository, ReglamentRepository>();
builder.Services.AddScoped<IPotReglamentRepository, PotReglamentRepository>();
builder.Services.AddScoped<IDeviationRepository, DeviationRepository>();
builder.Services.AddScoped<IDeviationValuesRepository, DeviationValuesRepository>();
builder.Services.AddScoped<IPotParametersRepository, PotParametersRepository>();
builder.Services.AddScoped<IExternalDataRepository, ExternalDataRepository>();

builder.Services.AddScoped(typeof(IGenericService<UserDto>), typeof(GenericService<User, UserDto>));
builder.Services.AddScoped(typeof(IGenericService<BuildingDto>), typeof(GenericService<Building, BuildingDto>));
builder.Services.AddScoped(typeof(IGenericService<ReglamentDto>), typeof(GenericService<Reglament, ReglamentDto>));
builder.Services.AddScoped(typeof(IGenericService<MetalMarkDto>), typeof(GenericService<MetalMark, MetalMarkDto>));
builder.Services.AddScoped(typeof(IGenericService<CalculatedTaskDto>), typeof(GenericService<CalculatedTask, CalculatedTaskDto>));

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DeviationValuesService>();
builder.Services.AddScoped<PotParametersService>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MetalTapping API V1");
        c.RoutePrefix = "api/swagger";
    });

}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();