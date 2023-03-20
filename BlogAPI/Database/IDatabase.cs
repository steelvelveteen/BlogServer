using System.Collections.Generic;
using System.Threading.Tasks;

namespace YourProject.Data
{
	public interface IDatabase<T> where T : class
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetByIdAsync(int id);
		Task InsertAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(T entity);
	}
}
