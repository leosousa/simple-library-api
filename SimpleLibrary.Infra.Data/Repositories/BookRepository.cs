using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using SimpleLibrary.Domain.Entities;
using SimpleLibrary.Domain.Interfaces.Repositories;
using SimpleLibrary.Infra.Data.Contexts;

namespace SimpleLibrary.Infra.Data.Repositories;

public class BookRepository : IBookRepository
{
    protected readonly SimpleLibraryContext Db;
    protected readonly DbSet<Book> DbSet;

    public IUnitOfWork UnitOfWork => Db;

    public BookRepository(SimpleLibraryContext context)
    {
        Db = context;
        DbSet = Db.Set<Book>();
    }

    public async Task<Book> GetByIsbn(string isbn)
    {
        return await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.ISBN == isbn);
    }

    public async Task<Book> GetById(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<IEnumerable<Book>> GetAll()
    {
        return await DbSet.ToListAsync();
    }

    public void Add(Book book)
    {
        DbSet.Add(book);
    }

    public void Update(Book book)
    {
        DbSet.Update(book);
    }

    public void Remove(Book book)
    {
        DbSet.Remove(book);
    }

    public void Dispose()
    {
        Db.Dispose();
    }
}
