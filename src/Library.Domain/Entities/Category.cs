using Library.Domain.Common;

namespace Library.Domain.Entities;

public sealed class Category : BaseEntity
{
	public string Name { get; set; } = default!;
	public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
}
