using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Library.Infrastructure.Persistence;
public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}
	public DbSet<Book> Books => Set<Book>();
	public DbSet<Category> Categories => Set<Category>();
	public DbSet<BookCategory> BookCategories => Set<BookCategory>();
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
	}
}
