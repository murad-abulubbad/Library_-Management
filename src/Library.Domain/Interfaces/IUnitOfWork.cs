using Library.Domain.Entities;
namespace Library.Domain.Interfaces;
public interface IUnitOfWork
{
	IRepository<Book> Books { get; }
	void RemoveBookCategories(IEnumerable<BookCategory> relations);
	Task<int> SaveChangesAsync();
}
