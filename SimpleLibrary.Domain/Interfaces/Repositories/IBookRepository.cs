using NetDevPack.Data;
using SimpleLibrary.Domain.Entities;

namespace SimpleLibrary.Domain.Interfaces.Repositories;

public interface IBookRepository : IRepository<Book>
{
    Task<Book> GetByIsbn(string isbn);
    Task<Book> GetById(Guid id);
    Task<IEnumerable<Book>> GetAll();
    void Add(Book book);
    void Update(Book book);
    void Remove(Book book);
}
