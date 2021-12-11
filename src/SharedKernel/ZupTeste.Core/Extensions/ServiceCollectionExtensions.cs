using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ZupTeste.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddScopedByType(this IServiceCollection services, Type baseType, params Assembly[] assemblies)
    {
        return AddScopedByType(services, baseType, type => type.GetInterface($"I{type.Name}"), assemblies);
    }

    public static IServiceCollection AddScopedByType(this IServiceCollection services, Type baseType, Func<Type, Type> fromType, params Assembly[] assemblies)
    {
        foreach (var type in baseType.GetTypesFromAssemblies(assemblies))
        {
            services.AddScoped(fromType(type), type);
        }

        return services;
    }
        
    public static IServiceCollection AddTransientByType(this IServiceCollection services, Type baseType, params Assembly[] assemblies)
    {
        return AddTransientByType(services, baseType, type => type.GetInterface($"I{type.Name}"), assemblies);
    }
        
    public static IServiceCollection AddTransientByType(this IServiceCollection services, Type baseType, Func<Type, Type> fromType, params Assembly[] assemblies)
    {
        foreach (var type in baseType.GetTypesFromAssemblies(assemblies))
        {
            services.AddScoped(fromType(type), type);
        }

        return services;
    }
}