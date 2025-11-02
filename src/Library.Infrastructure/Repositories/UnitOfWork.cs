using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Persistence;
using Library.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
	private readonly AppDbContext _context;

	public UnitOfWork(AppDbContext context)
	{
		_context = context;

		Books = new EfRepository<Book>(_context);
		Categories = new EfRepository<Category>(_context);
		BookCategories = new EfRepository<BookCategory>(_context);
	}
	
	public IRepository<Book> Books { get; }
	public IRepository<Category> Categories { get; }
	public IRepository<BookCategory> BookCategories { get; }

	public async Task<int> SaveChangesAsync()
		=> await _context.SaveChangesAsync();
}
