using Netx.Ddd.Domain.Aggregates;

namespace Netx.Ddd.Domain;

public class DomainEvent : EventBase
{
    public DomainEvent(Guid aggregateid)
        :base((aggregateid))
    {
        Entities = new List<object>();
    }

    public List<object> Entities { get; set; }
}
