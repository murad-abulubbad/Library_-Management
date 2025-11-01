using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController<TEntityDto, TCreate> : ControllerBase
{
	private readonly dynamic _service;

	protected BaseController(dynamic service)
	{
		_service = service;
	}
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var items = await _service.ListAsync();
		return Ok(items);
	}
	[HttpPost]
	public async Task<IActionResult> Create([FromBody] TCreate req)
	{
		var created = await _service.CreateAsync(req);
		return Ok(created);
	}

	[HttpPut("{id:long}")]
	public async Task<IActionResult> Update(long id, [FromBody] TEntityDto req)
	{
		if ((long)req.GetType().GetProperty("Id")!.GetValue(req)! != id)
			return BadRequest("Id mismatch.");

		var updated = await _service.UpdateAsync(req);
		if (updated == null)
			return NotFound();

		return Ok(updated);
	}
	[HttpDelete("{id:long}")]
	public async Task<IActionResult> Delete(long id)
	{
		var result = await _service.DeleteAsync(id);
		if (!result)
			return NotFound();

		return Ok(true);
	}
}
