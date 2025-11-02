using Library.Application.DTOs.CategoryDTOs;
using Library.Application.Services.Interfaces;

namespace Library.API.Controllers;

public class CategoriesController
	: BaseController<CategoryDto, CreateCategory>
{
	public CategoriesController(ICategoriesService service) : base(service) { }

}
