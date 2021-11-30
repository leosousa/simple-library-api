using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLibrary.Domain.Events;

public class BookRegisteredEvent : Event
{
    public BookRegisteredEvent(Guid id, string title, DateTime publishedDate, string isbn, int edition)
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