namespace Netx.Ddd.Domain;

public abstract record DomainCommand : CommandBase, IDomainEvent
{
    private Guid _eventId;
    private DateTime _creationTime;

    protected DomainCommand()
        : this(Guid.NewGuid(), DateTime.UtcNow)
    { }

    protected DomainCommand(Guid eventId, DateTime creationTime)
    {
        _eventId = eventId;
        _creationTime = creationTime;
    }

    public Guid GetEventId() => _eventId;

    public void SetEventId(Guid eventId) => _eventId = eventId;

    public DateTime GetCreationTime() => _creationTime;

    public void SetCreationTime(DateTime creationTime) => _creationTime = creationTime;
}
