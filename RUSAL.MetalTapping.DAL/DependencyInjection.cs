using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Interfaces;
using RUSAL.MetalTapping.DAL.Repositories;

namespace RUSAL.MetalTapping.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
        }

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IDeviationRepository, DeviationRepository>();
        services.AddScoped<IDeviationValuesRepository, DeviationValuesRepository>();
        services.AddScoped<IExternalDataRepository, ExternalDataRepository>();
        services.AddScoped<IPotParametersRepository, PotParametersRepository>();
        services.AddScoped<ICalculatedTaskRepository, CalculatedTaskRepository>();
        services.AddScoped<IReglamentRepository, ReglamentRepository>();
        services.AddScoped<IPotReglamentRepository, PotReglamentRepository>();
        services.AddScoped<IMetalMarkAnalysisRepository, MetalMarkAnalysisRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IMetalMarkRepository, MetalMarkRepository>();
        services.AddScoped<IPotGroupRepository, PotGroupRepository>();
        services.AddScoped<IScoopUsageRepository, ScoopUsageRepository>();
        services.AddScoped<IShiftRepository, ShiftRepository>();
        services.AddScoped<ITasksRepository, TaskRepository>();
        services.AddScoped<ITapTaskPotRepository, TapTaskPotRepository>();
        services.AddScoped<IMetalMarkAnalysisValueRepository, MetalMarkAnalysisValueRepository>();
        services.AddScoped<IPotRepository, PotRepository>();

        return services;
    }
}
