using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.DAL.Repositories;

namespace RUSAL.MetalTapping.DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDAL(this IServiceCollection services, string? connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Connection string 'DefaultConnection' is not configured.", nameof(connectionString));
            }

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepositoryAdapter<>));

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
            services.AddScoped<IPotGroupHistoryRepository, PotGroupHistoryRepository>();
            services.AddScoped<IPotGroupRepository, PotGroupRepository>();
            services.AddScoped<IScoopUsageRepository, ScoopUsageRepository>();
            services.AddScoped<IShiftRepository, ShiftRepository>();

            return services;
        }
    }
}