using SimpleLibrary.Domain.Entities.Base;

namespace SimpleLibrary.Domain.Entities;

public class PublishingCompany : Entity
{
    protected PublishingCompany() { /* Required by EF */ }

    public PublishingCompany(string name, string cnpj)
    {
        Name = name;
        CNPJ = cnpj;
    }

    public string Name { get; private set; }
    public string CNPJ { get; private set; }
}