using NetDevPack.Messaging;

namespace SimpleLibrary.Domain.Events;

public class BookUpdatedEvent : Event
{
    public BookUpdatedEvent(Guid id, string title, DateTime publishedDate, string isbn, int edition)
    {
        Id = id;
        Title = title;
        PublishedDate = publishedDate;
        ISBN = isbn;
        Edition = edition;
        AggregateId = id;
    }

    public Guid Id { get; set; }

    public string Title { get; private set; }

    public DateTime PublishedDate { get; private set; }

    public string ISBN { get; private set; }

    public int Edition { get; private set; }
}
