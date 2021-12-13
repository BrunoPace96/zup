using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ZupTeste.Core.Contracts;

namespace ZupTeste.Infra.Data.Extensions;

public static class ChangeTrackerExtensions
    {
        public static ChangeTracker ApplyAudit(this ChangeTracker tracker)
        {
            tracker.DetectChanges();

            foreach (var entry in tracker.Entries())
            {
                if (entry.Entity is IAuditableEntity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property(nameof(IAuditableEntity.CreatedAt)).CurrentValue = DateTime.UtcNow;
                        entry.Property(nameof(IAuditableEntity.LastUpdatedAt)).CurrentValue = DateTime.UtcNow;
                    }

                    if (entry.State == EntityState.Modified)
                        entry.Property(nameof(IAuditableEntity.LastUpdatedAt)).CurrentValue = DateTime.UtcNow;
                }
            }

            return tracker;
        }
    }