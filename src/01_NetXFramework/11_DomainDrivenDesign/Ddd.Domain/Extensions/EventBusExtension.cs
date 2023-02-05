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
    /// <summary>
    /// 每一次commit代表一个event store过程
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="eventBus"></param>
    /// <param name="ctx"></param>
    /// <returns></returns>
    public static async Task PublishDomainEvents<T>(this IEventBus eventBus, T ctx) where T : DbContext
    {
        var entries = ctx.ChangeTracker.Entries();
        var aggregateid = Guid.NewGuid();
        var domainEvent = new DomainEvent(aggregateid);
        domainEvent.Entities.AddRange(entries.Select(p => new { p.CurrentValues.EntityType.Name, p.Entity, p.State }));
        await eventBus.PublishAsync(domainEvent);
    }
}
