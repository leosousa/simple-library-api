using NetDevPack.Domain;

namespace SimpleLibrary.Domain.Entities;

public class Book : Entity, IAggregateRoot
{
    protected Book() { /* Required by EF */ }

    public Book(Guid id, string title, DateTime publishDate, string isbn, int edition)
    {
        Id = id;
        Title = title;
        PublishDate = publishDate;
        ISBN = isbn;
        Edition = edition;
    }

    public string Title { get; private set; }
    public DateTime PublishDate { get; private set; }
    public string ISBN { get; private set; }
    public int Edition { get; private set; }

    public List<Author> Authors { get; private set; }

    public Guid IdPublishingCompany { get; private set; }
    public virtual PublishingCompany PublishingCompany { get; private set; }
}
