using ZupTeste.API.Authentication;
using ZupTeste.API.Common.Middlewares;
using ZupTeste.API.Common.Setup;
using ZupTeste.Infra.Data.Context;
using ZupTeste.Infra.DI;
using ZupTeste.Infra.Settings;

var builder = WebApplication.CreateBuilder(args);
var appSettings = builder.Configuration.Get<AppSettings>();

// Add services to the container.

builder.Services
    .AddCustomControllers()
    .AddApplicationDependencies(appSettings)
    .AddEndpointsApiExplorer()
    .AddJwtConfiguration(appSettings)
    .Configure<RouteOptions>(options =>
    {
        options.LowercaseUrls = true;
        options.LowercaseQueryStrings = true;
    });

if (!appSettings.IsTestEnv)
{
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

if (!appSettings.IsTestEnv)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Action<string> logger = Console.WriteLine;
app.UseMiddleware<LoggingMiddleware>(logger);

if (!appSettings.IsTestEnv)
{
    // Apply migration if necessary
    using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    scope.ServiceProvider.GetService<DatabaseContext>()!.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseJwt();

app.MapControllers();

app.Run();

public partial class Program { }