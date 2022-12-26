using Netx.Ddd.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Domain;

public record TestCommand : DomainCommand
{
    public string Name { get; set; }

    public TestCommand(Guid eventId, DateTime creationTime, string name) 
        : base(eventId, creationTime)
    {
        Name = name;
    }
}
