using Netx.Ddd.Domain.Aggregates;

namespace Netx.Ddd.Domain;

public abstract class DomainEvent : EventBase
{
    public DomainEvent(Guid aggregateid)
        :base((aggregateid))
    {

    }

    public object Entity { get; set; }
}
