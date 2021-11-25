using SimpleLibrary.Domain.Entities.Base;

namespace SimpleLibrary.Domain.Entities;

public class Author : Entity
{
    public string Name { get; set; }
    public string Bio { get; set; }

    public List<Book> Books { get; set; }
}
