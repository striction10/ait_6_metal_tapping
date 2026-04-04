using Microsoft.Extensions.DependencyInjection;
using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Application.UseCases;
using RUSAL.MetalTapping.BLL.Application.UseCases.Buildings;

public static class DependencyInjection
{
    public static IServiceCollection AddBLL(this IServiceCollection services)
    {

        services.AddScoped<DeviationCalculationService>();
        services.AddScoped<CalculatedTaskService>();
        services.AddScoped<PotParametersService>();
        services.AddScoped<PotViewService>();
        

        services.AddScoped<ProcessDeviationAndTaskUseCase>();
        services.AddScoped<ProcessCalculatedTaskUseCase>();
        services.AddScoped<ViewDeviationAndTaskUseCase>();
        services.AddScoped<RegisterUserUseCase>();
        services.AddScoped<LoginUserUseCase>();
        services.AddScoped<ProcessRoundTaskUseCase>();
        services.AddScoped<GetAllReglamentsUseCase>();
        services.AddScoped<GetAllMetalMarksUseCase>();
        services.AddScoped<GetAllBuildingsUseCase>();
        services.AddScoped<DeviationValuesUseCase>();
        services.AddScoped<CreateTaskUseCase>();
        services.AddScoped<CastingBuildingSelectorService>();

        return services;
    }
}