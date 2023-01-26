using System.Linq.Expressions;

namespace ApiRepository.Interfaces;

public interface IRepository<T> : IDisposable
    where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetByID(int? id);
    Task<T> Insert(T entity);
    Task<T> Delete(int id);
    Task Update(T entity);
    Task<T> Find(Expression<Func<T, bool>> expr);
}
