namespace NetX.Ddd.Domain;

public interface IEventStoreRepository : IDisposable
{
    void Store(StoredEvent theEvent);

    Task<IList<StoredEvent>> All(Guid aggregateId);
}
