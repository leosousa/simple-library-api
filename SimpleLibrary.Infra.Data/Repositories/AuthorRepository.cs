using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using SimpleLibrary.Domain.Entities;
using SimpleLibrary.Domain.Interfaces.Repositories;
using SimpleLibrary.Infra.Data.Contexts;
using SimpleLibrary.Infra.Data.Repositories.Base;

namespace SimpleLibrary.Infra.Data.Repositories;

public class AuthorRepository : IAuthorRepository
{
    protected readonly SimpleLibraryContext Db;
    protected readonly DbSet<Book> DbSet;

    public IUnitOfWork UnitOfWork => Db;

    public AuthorRepository(SimpleLibraryContext context)
    {
        Db = context;
        DbSet = Db.Set<Book>();
    }

    public void Dispose()
    {
        Db.Dispose();
    }
}
