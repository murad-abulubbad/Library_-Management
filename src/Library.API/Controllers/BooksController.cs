using Library.Application.DTOs.BookDTOs;
using Library.Application.Services.Interfaces;
using Library.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
namespace Library.API.Controllers;
public class BooksController
	: BaseController<BookDto, CreateBook>
{
	private readonly IBooksService _service;

	public BooksController(IBooksService service) : base(service)
	{
		_service = service;
	}

	[HttpGet("{id:long}")]
	public async Task<IActionResult> GetById(long id)
	{
		var book = await _service.GetByIdAsync(id);
		if (book == null)
			return NotFound();

		return Ok(book);
	}
	[HttpGet("with-categories-ado")]
	public async Task<IActionResult> GetAllWithCategoriesAdo([FromServices] AdoBookRepository ado)
	{
		var result = await ado.GetAllBooksWithCategoriesAsync();
		return Ok(result);
	}
}
