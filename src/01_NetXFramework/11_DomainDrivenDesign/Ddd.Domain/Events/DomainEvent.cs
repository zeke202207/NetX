using NetX.Ddd.Domain.Aggregates;

namespace NetX.Ddd.Domain;

public class DomainEvent : EventBase
{
    public DomainEvent(Guid aggregateid)
        :base((aggregateid))
    {
        Entities = new List<object>();
    }

    public List<object> Entities { get; set; }
}
