using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleLibrary.Domain.Entities;

namespace SimpleLibrary.Infra.Data.Mappings;

public class PublishingCompanyMap : IEntityTypeConfiguration<PublishingCompany>
{
	public void Configure(EntityTypeBuilder<PublishingCompany> builder)
	{
		builder.HasKey(c => c.Id);

		builder.Property(c => c.Name).HasMaxLength(100).IsRequired(true);
		builder.Property(c => c.CNPJ).HasMaxLength(14).IsRequired(true);

		builder.ToTable("PublishingCompany");
	}
}
