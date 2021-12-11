using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ZupTeste.Infra.IoC.Setup
{
    public static class ValidationSetup
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.Load("ZupTeste.Domain"));

            return services;
        }
    }
}