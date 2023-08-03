using MediatR;
using NetX.Common.Attributes;
using NetX.Tenants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Ddd.Domain;

[Scoped]
public class EventStoreEvent : IEventBus
{
    /// <summary>
    /// 用于事件传递
    /// 在读写分离数据库设计下
    /// 写库成功后，通过<see cref="IMediator"/> 进行广播，通知读库更新
    /// </summary>
    private readonly IMediator mediator;
    private readonly IEventStoreRepository _eventStoreRep;

    public EventStoreEvent(
        IMediator mediator,
        IEventStoreRepository eventStoreRep
    )
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this._eventStoreRep = eventStoreRep;
    }

    public async Task PublishAsync(params EventBase[] events)
    {
        foreach (var @event in events)
        {
            var domainEvent = @event as DomainEvent;
            if (null == domainEvent)
                continue;
            var userId = TenantContext.CurrentTenant.Principal?.UserId ?? "netx-zeke";
            var storedEvent = new StoredEvent(@event, JsonConvert.SerializeObject(domainEvent.Entities, new StringEnumConverter()), userId);
            this._eventStoreRep.Store(storedEvent);
            await mediator.Publish(@event);
        }
    }
}
