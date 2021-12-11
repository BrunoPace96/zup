using ZupTeste.API.Common.Filters;

namespace ZupTeste.API.Common.Setup
{
    public static class ControllersSetup
    {
        public static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {
            services.AddControllers(e =>
            {
                e.Filters.Add<DomainValidationFilter>();
            }).ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressMapClientErrors = true;
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }
    }
}