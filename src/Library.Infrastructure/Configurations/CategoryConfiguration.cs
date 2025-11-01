using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Library.Infrastructure.Configurations;
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder.ToTable("Categories");

		builder.HasKey(c => c.Id);

		builder.Property(c => c.Name)
			   .IsRequired()
			   .HasMaxLength(100);

		builder.Property(c => c.CreatedAt)
			   .HasColumnType("datetime2");

		builder.HasMany(c => c.BookCategories)
			   .WithOne(bc => bc.Category)
			   .HasForeignKey(bc => bc.CategoryId);
	}
}
