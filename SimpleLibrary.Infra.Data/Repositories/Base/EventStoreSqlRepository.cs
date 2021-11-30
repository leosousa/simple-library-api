using SimpleLibrary.Domain.Interfaces.Repositories.Base;
using SimpleLibrary.Domain.Shared.Events;
using SimpleLibrary.Infra.Data.Contexts;

namespace SimpleLibrary.Infra.Data.Repositories.Base;

public class EventStoreSqlRepository : IEventStoreRepository
{
    private readonly EventStoreSqlContext _context;

    public EventStoreSqlRepository(EventStoreSqlContext context)
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
