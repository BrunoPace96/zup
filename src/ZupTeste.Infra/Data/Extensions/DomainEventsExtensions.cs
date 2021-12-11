using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ZupTeste.Core.Contracts;

namespace ZupTeste.Infra.Data.Extensions;

public static class DomainEventsExtensions
{
    public static async Task DispatchDomainEventsAsync(
        this IMediator content,
        DbContext context
    )
    {
        var list1 = context.ChangeTracker
            .Entries<IEntityBase>()
            .Where((Func<EntityEntry<IEntityBase>, bool>) (e => 
                e.Entity.DomainEvents != null && 
                e.Entity.DomainEvents.Any()))
            .ToList();
            
        var list2 = list1
            .SelectMany((Func<EntityEntry<IEntityBase>, IEnumerable<INotification>>) (e => e.Entity.DomainEvents))
            .ToList();
            
        list1.ForEach((Action<EntityEntry<IEntityBase>>) 
            (e => e.Entity.ClearDomainEvents()));
            
        await Task.WhenAll(list2.Select((Func<INotification, Task>) 
            (async domainEvent => await content.Publish(domainEvent))));
    }
}