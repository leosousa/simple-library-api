using System.Transactions;

namespace SimpleLibrary.Domain.Interfaces.Repositories.Base;

public interface IUnitWork
{
    IBookRepository bookRepository { get; }
    IAuthorRepository authorRepository { get; }
    IPublishingCompanyRepository publishingCompanyRepository { get; }

    TransactionScope GetTransaction();
    int SaveChanges();
    Task<int> SaveChangesAsync();
}
