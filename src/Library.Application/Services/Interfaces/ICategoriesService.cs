using Library.Application.DTOs.CategoryDTOs;
using Library.Domain.Entities;

namespace Library.Application.Services.Interfaces;

public interface ICategoriesService
{
	Task<IReadOnlyList<CategoryDto>> ListAsync();
	Task<Category> CreateAsync(CreateCategory req);
	Task<Category?> UpdateAsync(CategoryDto req);
	Task<bool> DeleteAsync(long id);
}
