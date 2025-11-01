namespace Library.Application.DTOs.BookDTOs;
public record MiniBookDto(
	long Id,
	string Title,
	string Author,
	int? Year
);
