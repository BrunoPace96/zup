using Microsoft.Extensions.DependencyInjection;
using ZupTeste.DomainValidation.DI;
using ZupTeste.Infra.Data.Repositories;
using ZupTeste.Infra.Data.UnitOfWork;
using ZupTeste.Infra.IoC.Setup;
using ZupTeste.Infra.Settings;
using ZupTeste.Repository.DI;

namespace ZupTeste.Infra.IoC;

public static class Injector
{
    public static IServiceCollection AddApplicationDependencies(
        this IServiceCollection services,
        AppSettings appSettings
    )
    {
        services
            .AddRepositories(typeof(ReadOnlyRepository<>), typeof(Repository<>), typeof(UnitOfWorkScopeFactory))
            .AddDomainValidation()
            .AddMediator()
            .AddValidation()
            .AddMapping()
            .AddSingleton(appSettings);

        if (!appSettings.IsTestEnv)
        {
            services
                .AddDatabase(appSettings);
        }
            
        return services;
    } 
}