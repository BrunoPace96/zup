using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace ZupTeste.Infra.Data.Extensions;

public static class DatabaseContextExtensions
{
    public static ModelBuilder ConfigureDefaultStringProperties(this ModelBuilder modelBuilder)
    {
        var properties = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)));
            
        foreach (var property in properties)
            property.SetColumnType("varchar(512)");

        return modelBuilder;
    }
        
    public static void ConfigureMapping(this ModelBuilder builder, Type type) => 
        builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(type)!);
}