namespace Library.Application.DTOs.BookDTOs;

public record BookDto(
	long Id,
	string Title,
	string Author,
	int? Year,
	List<long> CategoryIds
);
