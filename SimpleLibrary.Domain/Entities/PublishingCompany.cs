
using NetDevPack.Domain;

namespace SimpleLibrary.Domain.Entities;

public class PublishingCompany : Entity, IAggregateRoot
{
    protected PublishingCompany() { /* Required by EF */ }

    public PublishingCompany(Guid id, string name, string cnpj)
    {
        Id = id;
        Name = name;
        CNPJ = cnpj;
    }

    public string Name { get; private set; }
    public string CNPJ { get; private set; }
}