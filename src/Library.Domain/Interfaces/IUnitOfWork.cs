using Library.Domain.Entities;

namespace Library.Domain.Interfaces;

public interface IUnitOfWork
{
	IRepository<Book> Books { get; }
	IRepository<Category> Categories { get; }
	IRepository<BookCategory> BookCategories { get; }

	Task<int> SaveChangesAsync();
}
