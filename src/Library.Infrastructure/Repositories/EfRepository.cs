using Library.Domain.Interfaces;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Infrastructure.Repositories;

public class EfRepository<T> : IRepository<T> where T : class
{
	private readonly AppDbContext _context;

	public EfRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task AddAsync(T entity)
		=> await _context.Set<T>().AddAsync(entity);

	public async Task DeleteAsync(T entity)
	{
		_context.Set<T>().Remove(entity);
		await Task.CompletedTask; // ✅ لتخليها متوافقة مع async
	}

	public async Task<T?> GetByIdAsync(long id)
		=> await _context.Set<T>().FindAsync(id);

	public async Task<IReadOnlyList<T>> ListAllAsync(
		Expression<Func<T, bool>>? filter = null,
		params Expression<Func<T, object>>[] includes)
	{
		IQueryable<T> query = _context.Set<T>();

		if (filter != null)
			query = query.Where(filter);

		foreach (var include in includes)
			query = query.Include(include);

		return await query.ToListAsync();
	}

	public async Task UpdateAsync(T entity)
	{
		_context.Set<T>().Update(entity);
		await Task.CompletedTask;
	}

	public IQueryable<T> Query()
		=> _context.Set<T>().AsQueryable();
}
