using Library.Application.DTOs.CategoryDTOs;
public record BookWithCategoriesDto(
	long Id,
	string Title,
	string Author,
	int? Year,
	List<CategoryDto> Categories
);
