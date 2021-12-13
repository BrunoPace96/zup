using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ZupTeste.Domain.Administradores;

namespace ZupTeste.Infra.Data.Context;

public static class AdministradoresSeed
{
    public static ModelBuilder SeedAdministradores(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrador>().HasData(new Administrador
        {
            Id = Guid.NewGuid(),
            Nome = "admin",
            Email = "admin@admin.com",
            Senha = "b16f5428b3b26c8782e791dc4261f57fb54847ce8372694e6841553edf16ab26;Jhhz7rO40tPtKqk",
            CreatedAt = new DateTime(2021, 03, 08, 12, 00, 00, DateTimeKind.Utc),
            LastUpdatedAt = new DateTime(2021, 03, 08, 12, 00, 00, DateTimeKind.Utc)
        });

        return modelBuilder;
    }
}