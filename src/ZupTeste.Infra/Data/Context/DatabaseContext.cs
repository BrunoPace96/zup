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

        builder.SeedAdministradores();
        
        base.OnModelCreating(builder);
    }

    public void ApplyMigrations()
    {   
        if (Database.GetPendingMigrations().Any())
            Database.Migrate();
    }
    
    public override int SaveChanges()
    {
        ChangeTracker
            .ApplyAudit();
        
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker
            .ApplyAudit();
        
        return await base.SaveChangesAsync(cancellationToken);
    }
}