using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class GenericRepositoryAdapter<TDomain> : IGenericRepository<TDomain>
{
    private readonly IGenericRepository<TDomain> _inner;

    public GenericRepositoryAdapter(IServiceProvider provider, IMapper mapper)
    {
        var config = mapper?.ConfigurationProvider
            ?? throw new ArgumentNullException(nameof(mapper), "IMapper is not registered in DI.");

        Type? entityType = null;

        var typeMapsProp = config.GetType()
            .GetProperty("TypeMaps", BindingFlags.NonPublic | BindingFlags.Instance);

        if (typeMapsProp != null)
        {
            var typeMaps = typeMapsProp.GetValue(config) as IEnumerable<object>;
            var typeMapObj = typeMaps?
                .FirstOrDefault(tm =>
                {
                    var srcProp = tm?.GetType().GetProperty("SourceType", BindingFlags.Public | BindingFlags.Instance);
                    var srcType = srcProp?.GetValue(tm) as Type;
                    return srcType == typeof(TDomain);
                });

            if (typeMapObj != null)
            {
                var destProp = typeMapObj.GetType().GetProperty("DestinationType", BindingFlags.Public | BindingFlags.Instance);
                entityType = destProp?.GetValue(typeMapObj) as Type;
            }
        }

        if (entityType == null)
        {
            var expectedName = typeof(TDomain).Name + "Model";
            entityType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a =>
                {
                    try { return a.GetTypes(); }
                    catch { return Array.Empty<Type>(); }
                })
                .FirstOrDefault(t => t.Name == expectedName);
        }

        if (entityType == null)
        {
            throw new InvalidOperationException(
                $"AutoMapper type map not found for domain type {typeof(TDomain).FullName}. " +
                "Register a map from the domain type to the EF entity or provide an explicit repository registration. " +
                $"(Tried AutoMapper TypeMaps and fallback to convention '{typeof(TDomain).Name}Model'.)");
        }

        var repoType = typeof(GenericRepository<,>).MakeGenericType(typeof(TDomain), entityType);
        _inner = (IGenericRepository<TDomain>)ActivatorUtilities.CreateInstance(provider, repoType);
    }

    public Task<TDomain?> GetByIdAsync(Guid id) => _inner.GetByIdAsync(id);
    public Task<IEnumerable<TDomain>> GetAllAsync() => _inner.GetAllAsync();
    public Task CreateAsync(TDomain domain) => _inner.CreateAsync(domain);
    public Task UpdateAsync(TDomain domain) => _inner.UpdateAsync(domain);
    public Task DeleteAsync(Guid id) => _inner.DeleteAsync(id);
    public Task SaveChangesAsync() => _inner.SaveChangesAsync();
}