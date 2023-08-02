namespace NetX.Ddd.Core;

public interface IDomainEvent
{
    Guid GetEventId();

    void SetEventId(Guid eventId);

    DateTime GetCreationTime();
}
