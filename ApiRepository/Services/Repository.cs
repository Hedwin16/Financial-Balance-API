using ApiRepository.Interfaces;
using DB.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiRepository.Services;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApiContext context;

    public Repository(ApiContext context)
    {
        this.context = context;
    }

    protected DbSet<T> EntitySet
    {
        get
        {
            return context.Set<T>();
        }
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await EntitySet.ToListAsync();
    }

    public async Task<T> GetByID(int? id)
    {
        return await EntitySet.FindAsync(id);
    }

    public async Task<T> Insert(T entity)
    {
        EntitySet.Add(entity);
        await Save();
        return entity;
    }

    public async Task<T> Delete(int id)
    {
        T entity = await EntitySet.FindAsync(id);
        EntitySet.Remove(entity);
        await Save();
        return entity;
    }

    public async Task Update(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        await Save();
    }

    public async Task Save()
    {
        await context.SaveChangesAsync();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed && disposing)
        {
            context.Dispose();
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<T> Find(Expression<Func<T, bool>> expr)
    {
        return await EntitySet.AsNoTracking().FirstOrDefaultAsync(expr);
    }
}