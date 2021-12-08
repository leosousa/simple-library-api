using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using SimpleLibrary.Domain.Entities;
using SimpleLibrary.Domain.Interfaces.Repositories;
using SimpleLibrary.Infra.Data.Contexts;

namespace SimpleLibrary.Infra.Data.Repositories;

public class PublishingCompanyRepository : IPublishingCompanyRepository
{
    protected readonly SimpleLibraryContext Db;
    protected readonly DbSet<PublishingCompany> DbSet;

    public IUnitOfWork UnitOfWork => Db;

    public PublishingCompanyRepository(SimpleLibraryContext context)
    {
        Db = context;
        DbSet = Db.Set<PublishingCompany>();
    }

    public void Dispose()
    {
        Db.Dispose();
    }
}
