
namespace Library.Domain.Entities;

public sealed class BookCategory
{
	public long BookId { get; set; }
	public Book Book { get; set; } = default!;

	public long CategoryId { get; set; }
	public Category Category { get; set; } = default!;
}