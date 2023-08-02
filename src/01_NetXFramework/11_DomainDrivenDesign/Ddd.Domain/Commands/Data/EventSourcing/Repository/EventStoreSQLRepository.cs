using NetX.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetX.Ddd.Domain;

[Scoped]
public class EventStoreSQLRepository : IEventStoreRepository
{
    private readonly EventStoreSQLContext _context;

    public EventStoreSQLRepository(EventStoreSQLContext context)
    {
        _context = context;
    }

    public async Task<IList<StoredEvent>> All(Guid aggregateId)
    {
        return await (from e in _context.StoredEvent where e.AggregateId == aggregateId select e).ToListAsync();
    }

    public void Store(StoredEvent theEvent)
    {
        _context.StoredEvent.Add(theEvent);
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
