using Microsoft.EntityFrameworkCore;
using ZupTeste.Infra.Data.Extensions;

namespace ZupTeste.Infra.Data.Context;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ConfigureMapping(GetType());

        builder
            .ConfigureDefaultStringProperties();

        base.OnModelCreating(builder);
    }

    public void ApplyMigrations()
    {
        if (Database.GetPendingMigrations().Any())
            Database.Migrate();
    }
}