using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZupTeste.Repository.Repository;
using ZupTeste.Repository.UnitOfWork.Factories;

namespace ZupTeste.Repository.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(
            this IServiceCollection services,
            Type readOnlyRepositoryImplementation,
            Type repositoryImplementation,
            Type unitOfWorkScopeFactory
        )
        {
            services.AddTransient(typeof(IReadOnlyRepository<>), readOnlyRepositoryImplementation);
            services.AddTransient(typeof(IRepository<>), repositoryImplementation);
            services.AddScoped(typeof(IUnitOfWorkScopeFactory), unitOfWorkScopeFactory);
            services.AddTransient<ISpecificationEvaluator, SpecificationEvaluator>();
            return services;
        }
    }
}