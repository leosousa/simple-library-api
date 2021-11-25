using SimpleLibrary.Domain.Entities;
using SimpleLibrary.Domain.Interfaces.Repositories;
using SimpleLibrary.Infra.Data.Contexts;
using SimpleLibrary.Infra.Data.Repositories.Base;

namespace SimpleLibrary.Infra.Data.Repositories;

public class PublishingCompanyRepository : Repository<SimpleLibraryContext, PublishingCompany>, IPublishingCompanyRepository
{
    public PublishingCompanyRepository(SimpleLibraryContext database)
        : base(database)
    {
    }
}
