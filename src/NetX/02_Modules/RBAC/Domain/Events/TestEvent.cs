using Netx.Ddd.Domain;

namespace NetX.RBAC.Domain;

public class TestEvent : DomainEvent
{
    public TestEvent(Guid aggregateid)
        : base(aggregateid)
    {

    }
}

public class TestEvent1 : DomainEvent
{
    public TestEvent1(Guid aggregateid)
        : base(aggregateid)
    {

    }
}
