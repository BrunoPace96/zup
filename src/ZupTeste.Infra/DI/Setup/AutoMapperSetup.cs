using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ZupTeste.Infra.DI.Setup
{
    public static class AutoMapperSetup
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.Load("ZupTeste.Domain"));

            return services;
        }
    }
}

