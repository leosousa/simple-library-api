using NetDevPack.Messaging;

namespace SimpleLibrary.Domain.Events;

public class BookRemovedEvent : Event
{
    public BookRemovedEvent(Guid id)
    {
        Id = id;
        AggregateId = id;
    }

    public Guid Id { get; set; }
}
