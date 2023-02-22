using ApiRepository.Interfaces;
using DB.Models;
using System.Linq.Expressions;

namespace ApiRepository.Services;

public class UserRepository<T> : IRepository<T>
    where T : User
{
    private readonly ApiContext context;

    public UserRepository(ApiContext context)
    {
        this.context = context;
    }

    public Task<T> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public async Task<User> Find(Expression<Func<User, bool>> expr)
    {
        return await context.Users.FindAsync(expr);
    }

    public Task<T> Find(Expression<Func<T, bool>> expr)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<T> GetByID(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<T> Insert(T entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(T entity)
    {
        throw new NotImplementedException();
    }
}
