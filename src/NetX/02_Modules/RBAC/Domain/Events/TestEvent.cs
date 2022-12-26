using Netx.Ddd.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Domain;

public class TestEvent : DomainEvent
{
    public TestEvent(Guid aggregateid)
        :base(aggregateid)
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
