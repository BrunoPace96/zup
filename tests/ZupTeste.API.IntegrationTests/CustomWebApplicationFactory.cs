using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZupTeste.API.IntegrationTests.Generator;
using ZupTeste.Core.Extensions;
using ZupTeste.Domain.Administradores;
using ZupTeste.Infra.Data.Context;

namespace ZupTeste.API.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public IServiceProvider ServiceProvider { get; private set; }

    public Administrador AdministradorPadrao { get; set; }
    
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

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<DatabaseContext>();

                db.Database.EnsureCreated();

                AdministradorPadrao = new Administrador
                {
                    Id = Guid.NewGuid(),
                    Nome = "admin",
                    Email = "admin@admin.com",
                    Senha = "b16f5428b3b26c8782e791dc4261f57fb54847ce8372694e6841553edf16ab26;Jhhz7rO40tPtKqk",
                    CreatedAt = new DateTime(2021, 03, 08, 12, 00, 00),
                    LastUpdatedAt = new DateTime(2021, 03, 08, 12, 00, 00)
                };
                
                db.Set<Administrador>().Add(AdministradorPadrao);
            }

            ServiceProvider = services.BuildServiceProvider();
        }).UseEnvironment("IntegrationTest");
    }
}