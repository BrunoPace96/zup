using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ZupTeste.DomainValidation.Domain.Behaviours;

namespace ZupTeste.Infra.DI.Setup
{
    public static class MediatorSetup
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.Load("ZupTeste.Domain"));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FailFastBehavior<,>));

            return services;
        }
            
    }
}