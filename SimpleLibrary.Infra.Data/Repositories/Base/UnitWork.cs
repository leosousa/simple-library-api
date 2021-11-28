using Microsoft.EntityFrameworkCore;
using SimpleLibrary.Domain.Interfaces.Repositories;
using SimpleLibrary.Domain.Interfaces.Repositories.Base;
using System.Transactions;

namespace SimpleLibrary.Infra.Data.Repositories.Base
{
    public class UnitWork<TDatabaseContext> : IUnitWork where TDatabaseContext : DbContext
    {
        private TDatabaseContext _context;
        public IBookRepository bookRepository { get; private set; }
        public IAuthorRepository authorRepository { get; private set; }
        public IPublishingCompanyRepository publishingCompanyRepository { get; private set; }

        public UnitWork(TDatabaseContext context, 
            IBookRepository bookRepository, 
            IAuthorRepository authorRepository, 
            IPublishingCompanyRepository publishingCompanyRepository)
        {
            _context = context;
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.publishingCompanyRepository = publishingCompanyRepository;
        }

        public TransactionScope GetTransaction()
        {
            var options = new TransactionOptions();
            options.IsolationLevel = IsolationLevel.ReadCommitted;
            return new TransactionScope(TransactionScopeOption.Required, options);
        }

        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database error: ", ex.Message);

                if (ex.InnerException != null)
                {
                    Console.WriteLine("Database error: ", ex.InnerException.Message);
                }

                if (ex.InnerException != null && ex.InnerException.InnerException != null)
                {
                    Console.WriteLine("Database error: ", ex.InnerException.InnerException.Message);
                }

                if (ex.InnerException != null && ex.InnerException.InnerException != null && ex.InnerException.InnerException.InnerException != null)
                {
                    Console.WriteLine("Database error: ", ex.InnerException.InnerException.InnerException.Message);
                }
            }

            return 0;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Task.FromResult(SaveChanges());
        }
    }
}
