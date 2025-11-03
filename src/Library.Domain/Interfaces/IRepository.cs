using System.Linq.Expressions;

public interface IRepository<T> where T : class
{
	Task<T?> GetByIdAsync(long id);
	Task<IReadOnlyList<T>> ListAllAsync(Expression<Func<T, bool>>? filter = null,
										params Expression<Func<T, object>>[] includes);
	Task AddAsync(T entity);
	Task UpdateAsync(T entity);
	Task DeleteAsync(T entity);
	IQueryable<T> Query();
}
