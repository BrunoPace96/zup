using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ZupTeste.Infra.Data.Context;
using ZupTeste.Infra.Settings;

namespace ZupTeste.Infra.DI.Setup
{
    public static class DatabaseSetup
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            AppSettings appSettings
        ) =>
            services.AddDbContextPool<DatabaseContext>(e =>
                e.UseNpgsql(appSettings.DatabaseConnection.ConnectionString));
    }
}