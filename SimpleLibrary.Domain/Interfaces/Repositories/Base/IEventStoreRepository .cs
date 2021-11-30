using SimpleLibrary.Domain.Shared.Events;

namespace SimpleLibrary.Domain.Interfaces.Repositories.Base;

public interface IEventStoreRepository : IDisposable
{
    void Store(StoredEvent theEvent);
    Task<IList<StoredEvent>> All(Guid aggregateId);
}
