using DB.Models.Result;
using System.Linq.Expressions;

namespace ApiRepository.Interfaces;

public interface IRepository<T> : IDisposable
    where T : class
{
    Task<Result<IEnumerable<T>>> GetAll();
    Task<Result<T>> GetByID(int? id);
    Task<Result<T>> Insert(T entity);
    Task<Result<T>> Delete(int id);
    Task<Result<T>> Update(T entity);
    Task<Result<T>> Find(Expression<Func<T, bool>> expr);
}
