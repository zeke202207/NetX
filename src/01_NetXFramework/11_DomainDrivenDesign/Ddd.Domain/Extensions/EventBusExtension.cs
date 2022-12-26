using Netx.Ddd.Core;
using Netx.Ddd.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Netx.Ddd.Domain;

public static class EventBusExtension
{
    public static async Task PublishDomainEvents<T>(this IEventBus eventBus, T ctx) where T : DbContext
    {
        foreach(var entrity in ctx.ChangeTracker.Entries())
        {
            var entiityType = entrity.Entity.GetType();
            if(entiityType.BaseType.IsGenericType && entiityType.BaseType.GetGenericTypeDefinition() == typeof(BaseEntity<>))
            {
                MethodInfo method = entiityType.GetMethod("DequeueUncommittedEvents", new Type[] {  });
                if (null != method)
                {
                    var result = method.Invoke(entrity.Entity, null) as EventBase[];
                    if(null != result)
                    {
                        var tasks = result.Select(async (domainEvent) =>
                        {
                            await eventBus.PublishAsync(domainEvent);
                        });
                        await Task.WhenAll(tasks);
                    }
                }
            }
        }


        //var domainEvents = ctx.ChangeTracker
        //    .Entries<Aggregate>()
        //    .SelectMany(x => x.Entity.DequeueUncommittedEvents())
        //    .ToList();
        //var tasks = domainEvents
        //    .Select(async (domainEvent) =>
        //    {
        //        await eventBus.PublishAsync(domainEvent);
        //    });

        //await Task.WhenAll(tasks);
    }
}
