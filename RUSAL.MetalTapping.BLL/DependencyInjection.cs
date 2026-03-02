using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RUSAL.MetalTapping.BLL.Interfaces;
using RUSAL.MetalTapping.BLL.Mapping;
using RUSAL.MetalTapping.BLL.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddBLL(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));
        var serviceAssembly = Assembly.GetAssembly(typeof(GenericService<,>));
        if (serviceAssembly != null)
        {
            services.Scan(scan => scan
                .FromAssemblies(serviceAssembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IGenericService<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }
        else
        {
            serviceAssembly = typeof(GenericService<,>).Assembly;
            services.Scan(scan => scan
                .FromAssemblies(serviceAssembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IGenericService<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }
        return services;
    }
}