using MediatR;
using NetX.Common.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netx.Ddd.Domain;

[Scoped]
public class EventStoreEvent : IEventBus
{
    private readonly IMediator mediator;
    private readonly IEventStoreRepository _eventStoreRep;

    public EventStoreEvent(
        //IMediator mediator,
        IEventStoreRepository eventStoreRep
    )
    {
        //this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this._eventStoreRep = eventStoreRep;
    }

    public async Task PublishAsync(params EventBase[] events)
    {
        foreach (var @event in events)
        {
            var domainEvent = @event as DomainEvent;
            if (null == domainEvent)
                continue;
            var storedEvent = new StoredEvent(@event, JsonConvert.SerializeObject(domainEvent.Entity), "zeke");
            this._eventStoreRep.Store(storedEvent);
            //await mediator.Publish(@event);
        }
    }
}
