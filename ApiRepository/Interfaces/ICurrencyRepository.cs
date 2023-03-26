using DB.Models;
using DB.Models.Result;

namespace ApiRepository.Interfaces;

public interface ICurrencyRepository : IRepository<Currency>
{
    public Result<Currency> GetByID(int id);
}
