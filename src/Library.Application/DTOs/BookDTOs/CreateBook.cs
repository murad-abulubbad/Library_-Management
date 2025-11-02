namespace Library.Application.DTOs.BookDTOs;
public record CreateBook
(
	string Title,
	string Author,
	int? Year,
	List<long>? CategoryIds = null 

);