using System.Linq.Expressions;
namespace Library.Domain.Interfaces;
public interface IRepository<T> where T : class
{
	Task<T?> GetByIdAsync(long id);
	Task<IReadOnlyList<T>> ListAllAsync(Expression<Func<T, bool>>? filter = null,
		params Expression<Func<T, object>>[] includes);
	Task AddAsync(T entity);
	void UpdateAsync(T entity);
	void DeleteAsync(T entity);
	IQueryable<T> Query();
}
