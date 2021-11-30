using NetDevPack.Domain;

namespace SimpleLibrary.Domain.Entities;

public class Author : Entity, IAggregateRoot
{
    public Author(Guid id, string name, string bio)
    {
        Id = Id;
        Name = name;
        Bio = bio;
    }

    protected Author() { /* Required by EF */ }

    public string Name { get; private set; }
    public string Bio { get; private set; }
    public List<Book> Books { get; private set; }
}
