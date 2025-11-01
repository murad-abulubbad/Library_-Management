using Library.Application.DTOs.CategoryDTOs;
using Library.Application.Services;

namespace Library.API.Controllers;

public class CategoriesController
	: BaseController<CategoryDto, CreateCategory>
{
	public CategoriesController(CategoriesService service) : base(service) { }

}
