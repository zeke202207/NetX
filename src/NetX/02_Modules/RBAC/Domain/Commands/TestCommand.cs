using Netx.Ddd.Domain;

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
