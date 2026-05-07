using Microsoft.Extensions.DependencyInjection;
using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Application.UseCases;
using RUSAL.MetalTapping.BLL.Application.UseCases.Buildings;
using RUSAL.MetalTapping.BLL.Domain.Auth;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;

public static class DependencyInjection
{
    public static IServiceCollection AddBLL(this IServiceCollection services)
    {

        services.AddScoped<DeviationCalculationService>();
        services.AddScoped<CalculatedTaskService>();
        services.AddScoped<PotParametersService>();
        services.AddScoped<PotViewService>();
        services.AddScoped<BuildingMetalInfoService>();
        services.AddScoped<CastingBuildingSelectorService>();
        services.AddScoped<CastingGroupSelectorService>();
        services.AddScoped<CastingExecutionPlanService>();
        services.AddScoped<CastingPotsService>();
        services.AddScoped<TapTaskService>();
        services.AddScoped<PotService>();
        services.AddScoped<BuildingService>();
        services.AddScoped<GroupService>();
        services.AddScoped<ShiftTaskService>();
        services.AddScoped<ShiftAssignmentService>();
        services.AddScoped<ScoopReservationService>();
        services.AddScoped<ViewTaskService>();
        services.AddScoped<ChemicalElemService>();
        services.AddScoped<DeviationService>();
        services.AddScoped<DeviationValuesService>();
        services.AddScoped<ExternalDataService>();
        services.AddScoped<MetalMarkAnalysisService>();
        services.AddScoped<MetalMarkAnalysisValueService>();
        services.AddScoped<MetalMarkService>();
        services.AddScoped<OrderService>();
        services.AddScoped<PotGroupService>();
        services.AddScoped<PotReglamentService>();
        services.AddScoped<ReglamentService>();
        services.AddScoped<RoleService>();
        services.AddScoped<ScoopService>();
        services.AddScoped<ScoopStateService>();
        services.AddScoped<ScoopUsageService>();
        services.AddScoped<ShiftService>();
        services.AddScoped<TapTaskPotService>();
        services.AddScoped<TapTaskReservationService>();
        services.AddScoped<TasksService>();
        services.AddScoped<UserRoleMembersService>();
        services.AddScoped<UserService>();
        services.AddScoped<EmailSenderService>();

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

        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddHostedService<OrderQueueWorker>();

        return services;
    }
}