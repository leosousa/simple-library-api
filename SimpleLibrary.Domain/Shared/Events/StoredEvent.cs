using NetDevPack.Messaging;

namespace SimpleLibrary.Domain.Shared.Events;

public class StoredEvent : Event
{
    public StoredEvent(Event theEvent, string data, string user)
    {
        Id = Guid.NewGuid();
        AggregateId = theEvent.AggregateId;
        MessageType = theEvent.MessageType;
        Data = data;
        User = user;
    }

    protected StoredEvent() { /* Required by EF */ }

    public Guid Id { get; private set; }

    public string Data { get; private set; }

    public string User { get; private set; }
}
