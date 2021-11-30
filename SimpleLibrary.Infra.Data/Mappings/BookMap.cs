using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleLibrary.Domain.Entities;

namespace SimpleLibrary.Infra.Data.Mappings;

public class BookMap : IEntityTypeConfiguration<Book>
{
	public void Configure(EntityTypeBuilder<Book> builder)
	{
		builder.HasKey(c => c.Id);

		builder.Property(c => c.Title).HasMaxLength(100).IsRequired(true);
		builder.Property(c => c.ISBN).HasMaxLength(50).IsRequired(true);
		builder.Property(c => c.Edition).HasMaxLength(100).IsRequired(true);

		builder.HasMany(s => s.Authors)
			  .WithMany(g => g.Books);

		builder.ToTable("Book");
	}
}
