using DB.Models;
using DB.Models.Result;

namespace ApiRepository.Interfaces;

public interface IUserRepository
{
    public Task<Result<User>> SaveUser(User user, UserPrivilege userPrivilege);
    public Task<Result<List<User>>> GetAll();
    public Result<User> GetByName(User user);
    public Result<User> GetById(int id);
    public Result<List<User>> Find(Func<User, bool> predicate);
    public Result<bool> DeleteUser(User user);
    public Result<User> Login(User user);
    public Result<User> SignUp(User user);

}
