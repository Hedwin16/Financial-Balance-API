using ApiRepository.Interfaces;
using DB.Models;
using DB.Models.Result;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiRepository.Services;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly ApiContext context;

    public CurrencyRepository(ApiContext context)
    {
        this.context = context;
    }

    protected DbSet<Currency> EntitySet
    {
        get
        {
            return context.Set<Currency>();
        }
    }

    private bool disposed = false;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (!disposed && disposing)
        {
            context.Dispose();
        }
        disposed = true;
    }

    public Result<Currency> GetByID(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<IEnumerable<Currency>>> GetAll()
    {
        try
        {
            var currencies = await context.Currencies.ToListAsync();

            if (currencies is null)
            {
                return new ErrorResult<IEnumerable<Currency>>("Currencies are null");
            }

            if (currencies.Count <= 0)
            {
                return new NotFoundResult<IEnumerable<Currency>>("There's not currencies in the system.");
            }

            return new SuccessResult<IEnumerable<Currency>>(currencies);
        }
        catch (Exception ex)
        {
            return new ErrorResult<IEnumerable<Currency>>("An error ocurred" + ex.ToString());
        }
    }

    public Task<Result<Currency>> GetByID(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Currency>> Insert(Currency entity)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Currency>> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Currency>> Update(Currency entity)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Currency>> Find(Expression<Func<Currency, bool>> expr)
    {
        throw new NotImplementedException();
    }
}
