using Library.Domain.Common;

namespace Library.Domain.Entities;

public sealed class Book : BaseEntity
{
	public string Title { get; set; } = string.Empty;
	public string Author { get; set; } = string.Empty;
	public int? Year { get; set; }
	public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
}
