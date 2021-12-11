using System.Reflection;

namespace ZupTeste.Core.Extensions;

public static class TypeExtension
{
        
    public static IEnumerable<Type> GetTypesFromAssemblies(this Type baseType, params Assembly[] assemblies)
    {
        if (assemblies == null || !assemblies.Any())
        {
            assemblies = new[] { baseType.Assembly };
        }

        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes().Where(type => type.IsClass && !type.IsAbstract);
                
            if (baseType.IsInterface)
            {
                types = types.Where(type => type.GetInterfaces().Any(
                    generic => generic.IsGenericType && generic.GetGenericTypeDefinition() == baseType));
            }
            else
            {
                types = types.Where(type => type.BaseType != null && (
                    type.BaseType.IsGenericType &&
                    type.BaseType.GetGenericTypeDefinition() == baseType ||
                    type.BaseType == baseType));
            }

            foreach (var type in types)
            {
                yield return type;
            }
        }
    }
        
}