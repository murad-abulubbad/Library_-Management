using Library.Application.DTOs.BookDTOs;
using Library.Application.Services.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Library.Application.Services;
public class BooksService : IBooksService
{
	private readonly IRepository<Book> _booksRepo;
	private readonly IUnitOfWork _uow;

	public BooksService(IUnitOfWork uow)
	{
		_uow = uow;
		_booksRepo = _uow.Books; 
	}
	public async Task<IReadOnlyList<MiniBookDto>> ListAsync()
	{
		var books = await _booksRepo.ListAllAsync();

		return books.Select(b => new MiniBookDto(
			b.Id,
			b.Title,
			b.Author,
			b.Year
		)).ToList();
	}
	public async Task<BookDto?> GetByIdAsync(long id)
	{
		var book = await _uow.Books
			.Query()
			.Include(b => b.BookCategories)
			.FirstOrDefaultAsync(b => b.Id == id);

		if (book == null) return null;

		return new BookDto(
			book.Id,
			book.Title,
			book.Author,
			book.Year,
			book.BookCategories.Select(bc => bc.CategoryId).ToList()
		);
	}
	public async Task<BookDto> CreateAsync(CreateBook req)
	{
		var book = new Book
		{
			Title = req.Title,
			Author = req.Author,
			Year = req.Year,
			BookCategories = new List<BookCategory>()
		};
		if (req.CategoryIds is not null && req.CategoryIds.Any())
		{
			foreach (var id in req.CategoryIds.Where(x => x > 0))
			{
				book.BookCategories.Add(new BookCategory
				{
					CategoryId = id,
					Book = book
				});
			}
		}
		await _booksRepo.AddAsync(book);
		await _uow.SaveChangesAsync();

		return new BookDto(
			book.Id,
			book.Title,
			book.Author,
			book.Year,
			null
		);
	}
	public async Task<BookDto?> UpdateAsync(BookDto req)
	{
		var book = await _booksRepo
			.Query()
			.Include(b => b.BookCategories)
			.FirstOrDefaultAsync(b => b.Id == req.Id);

		if (book == null) return null;

		book.Title = req.Title;
		book.Author = req.Author;
		book.Year = req.Year;
		book.BookCategories.Clear();

		if (req.CategoryIds != null && req.CategoryIds.Any())
		{
			foreach (var id in req.CategoryIds)
			{
				book.BookCategories.Add(new BookCategory
				{
					BookId = book.Id,
					CategoryId = id
				});
			}
		}
		await _uow.SaveChangesAsync();
		return new BookDto(
			book.Id,
			book.Title,
			book.Author,
			book.Year,
			book.BookCategories.Select(bc => bc.CategoryId).ToList()
		);
	}
	public async Task<bool> DeleteAsync(long id)
	{
		var book = await _booksRepo.GetByIdAsync(id);
		if (book == null) return false;

		_booksRepo.DeleteAsync(book);
		await _uow.SaveChangesAsync();

		return true;
	}
}
