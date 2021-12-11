using ZupTeste.API.Common.Middlewares;
using ZupTeste.API.Common.Setup;
using ZupTeste.Infra.Data.Context;
using ZupTeste.Infra.IoC;
using ZupTeste.Infra.Settings;

var builder = WebApplication.CreateBuilder(args);
var appSettings = builder.Configuration.Get<AppSettings>();

// Add services to the container.

builder.Services
    .AddCustomControllers()
    .AddApplicationDependencies(appSettings)
    .AddEndpointsApiExplorer();

if (!appSettings.IsTestEnv)
{
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
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

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }