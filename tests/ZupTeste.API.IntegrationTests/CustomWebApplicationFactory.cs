using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZupTeste.Infra.Data.Context;

namespace ZupTeste.API.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
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
        }).UseEnvironment("IntegrationTest");
    }
}