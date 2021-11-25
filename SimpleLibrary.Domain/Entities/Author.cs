using SimpleLibrary.Domain.Entities.Base;

namespace SimpleLibrary.Domain.Entities;

public class Author : Entity
{
    public Author(string name, string bio)
    {
        Name = name;
        Bio = bio;
    }

    protected Author() { /* Required by EF */ }

    public string Name { get; private set; }
    public string Bio { get; private set; }
    public List<Book> Books { get; private set; }
}
