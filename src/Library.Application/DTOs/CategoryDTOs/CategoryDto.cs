namespace Library.Application.DTOs.CategoryDTOs;

public record CategoryDto
{
	public long Id { get; init; }
	public string Name { get; init; } = string.Empty;

}
