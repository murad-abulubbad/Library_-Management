using Library.Application.DTOs.CategoryDTOs;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Library.Application.Services;

public class CategoriesService
{
	private readonly IRepository<Category> _repo;
	private readonly IUnitOfWork _uow;

	public CategoriesService(IRepository<Category> repo, IUnitOfWork uow)
	{
		_repo = repo;
		_uow = uow;
	}
	public async Task<Category?> UpdateAsync(CategoryDto req)
	{
		var category = await _repo.GetByIdAsync(req.Id);
		if (category == null) return null;

		category.Name = req.Name;
		_repo.UpdateAsync(category);
		await _uow.SaveChangesAsync();
		return category;
	}

	public async Task<IReadOnlyList<CategoryDto>> ListAsync()
	{
		var categories = await _repo.Query()
			.Include(c => c.BookCategories)
			.ThenInclude(bc => bc.Book)
			.ToListAsync();

		return categories.Select(c => new CategoryDto
		{
			Id = c.Id,
			Name = c.Name,

		}).ToList();

	}
	public async Task<Category> CreateAsync(CreateCategory req)
	{
		var category = new Category { Name = req.Name };
		await _repo.AddAsync(category);
		await _uow.SaveChangesAsync();
		return category;
	}
	public async Task<bool> DeleteAsync(long id)
	{
		var category = await _repo.GetByIdAsync(id);
		if (category == null) return false;

		_repo.DeleteAsync(category);
		await _uow.SaveChangesAsync();

		return true;
	}

}
