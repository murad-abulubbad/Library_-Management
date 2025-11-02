using Library.Application.DTOs.BookDTOs;

namespace Library.Application.Services.Interfaces;

public interface IBooksService
{
	Task<IReadOnlyList<MiniBookDto>> ListAsync();
	Task<BookDto?> GetByIdAsync(long id);
	Task<BookDto> CreateAsync(CreateBook req);
	Task<BookDto?> UpdateAsync(BookDto req);
	Task<bool> DeleteAsync(long id);
}
