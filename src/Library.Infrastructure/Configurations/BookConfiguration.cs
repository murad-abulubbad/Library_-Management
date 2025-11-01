using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Library.Infrastructure.Configurations;
public class BookConfiguration : IEntityTypeConfiguration<Book>
{
	public void Configure(EntityTypeBuilder<Book> builder)
	{
		builder.ToTable("Books");
		builder.HasKey(b => b.Id);
		builder.Property(b => b.Title)
			.IsRequired()
			.HasMaxLength(200);
		builder.Property(b => b.Author)
			.IsRequired()
			.HasMaxLength(100);
		builder.Property(b => b.Year)
			.IsRequired(false);
		builder.Property(b => b.CreatedAt)
			   .HasColumnType("datetime2");
		builder.HasMany(b => b.BookCategories)
			   .WithOne(bc => bc.Book)
			   .HasForeignKey(bc => bc.BookId);
	}
}
