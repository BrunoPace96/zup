using Microsoft.Extensions.DependencyInjection;
using ZupTeste.DomainValidation.Domain;

namespace ZupTeste.DomainValidation.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomainValidation(this IServiceCollection services)
        {
            services.AddScoped<IDomainValidationProvider, DomainValidationProvider>();
            return services;
        }
    }
}