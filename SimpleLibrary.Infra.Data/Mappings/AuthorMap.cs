using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleLibrary.Domain.Entities;

namespace SimpleLibrary.Infra.Data.Mappings;

public class AuthorMap : IEntityTypeConfiguration<Author>
{
	public void Configure(EntityTypeBuilder<Author> builder)
	{
		builder.HasKey(c => c.Id);

		builder.Property(c => c.Name).HasMaxLength(100).IsRequired(true);
		builder.Property(c => c.Bio).HasMaxLength(500).IsRequired(true);

		builder.HasMany(s => s.Books)
			  .WithMany(g => g.Authors);

		builder.ToTable("Author");
	}
}
