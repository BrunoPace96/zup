using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZupTeste.API.IntegrationTests.Generator;
using ZupTeste.Core.Extensions;
using ZupTeste.Infra.Data.Context;

namespace ZupTeste.API.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public IServiceProvider ServiceProvider { get; private set; }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();
            
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<DatabaseContext>));

            services.Remove(descriptor);

            services.AddScopedByType(
                typeof(BaseGenerator<>),
                type => type,
                Assembly.Load("ZupTeste.Api.IntegrationTests"));
            
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
                options.UseInternalServiceProvider(serviceProvider);
            });

            // var sp = services.BuildServiceProvider();
            //
            // using (var scope = sp.CreateScope())
            // {
            //     var scopedServices = scope.ServiceProvider;
            //     var db = scopedServices.GetRequiredService<DatabaseContext>();
            //     var logger = scopedServices
            //         .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
            //
            //     db.Database.EnsureCreated();
            //
            //     try
            //     {
            //         Utilities.InitializeDbForTests(db);
            //     }
            //     catch (Exception ex)
            //     {
            //         logger.LogError(ex, "An error occurred seeding the " +
            //                             "database with test messages. Error: {Message}", ex.Message);
            //     }
            // }
            
            ServiceProvider = services.BuildServiceProvider();
        }).UseEnvironment("IntegrationTest");
    }
}