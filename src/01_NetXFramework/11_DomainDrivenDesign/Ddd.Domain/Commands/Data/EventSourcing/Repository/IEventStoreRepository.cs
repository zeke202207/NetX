using System;
using System.Collections.Generic;
using System.Text;

namespace Netx.Ddd.Domain;

public interface IEventStoreRepository : IDisposable
{
    void Store(StoredEvent theEvent);

    Task<IList<StoredEvent>> All(Guid aggregateId);
}
