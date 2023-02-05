namespace Netx.Ddd.Domain;

public abstract class DomainQuery<TReponse> : QueryBase<TReponse>, IDomainEvent
{
    private Guid _eventId;
    private DateTime _creationTime;

    protected DomainQuery()
        : this(Guid.NewGuid(), DateTime.UtcNow)
    { }

    protected DomainQuery(Guid eventId, DateTime creationTime)
    {
        _eventId = eventId;
        _creationTime = creationTime;
    }

    public Guid GetEventId() => _eventId;

    public void SetEventId(Guid eventId) => _eventId = eventId;

    public DateTime GetCreationTime() => _creationTime;

    public void SetCreationTime(DateTime creationTime) => _creationTime = creationTime;
}
