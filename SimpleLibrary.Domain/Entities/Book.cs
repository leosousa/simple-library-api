using SimpleLibrary.Domain.Entities.Base;

namespace SimpleLibrary.Domain.Entities;

public class Book : Entity
{
    protected Book() { /* Required by EF */ }

    public Book(string title, DateTime publishDate, string iSBN, string edition, List<Author> authors, Guid idPublishingCompany, PublishingCompany publishingCompany)
    {
        Title = title;
        PublishDate = publishDate;
        ISBN = iSBN;
        Edition = edition;
        Authors = authors;
        IdPublishingCompany = idPublishingCompany;
        PublishingCompany = publishingCompany;
    }

    public string Title { get; private set; }
    public DateTime PublishDate { get; private set; }
    public string ISBN { get; private set; }
    public string Edition { get; private set; }

    public List<Author> Authors { get; private set; }

    public Guid IdPublishingCompany { get; private set; }
    public virtual PublishingCompany PublishingCompany { get; private set; }
}
