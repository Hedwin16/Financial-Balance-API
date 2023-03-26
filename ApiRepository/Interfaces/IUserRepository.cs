using DB.Models;
using DB.Models.Result;

namespace ApiRepository.Interfaces;

public interface IUserRepository
{
    public Task<Result<User>> SaveUser(User user, UserPrivilege userPrivilege);
    public Task<Result<List<User>>> GetAll();
    public Task<Result<User>> GetByName(User user);
    public Task<Result<User>> GetById(int id);
    public Task<Result<List<User>>> Find(Func<User, bool> predicate);
    public Task<Result<bool>> DeleteUser(User user);
    public Task<Result<User>> Login(User user);
    public Task<Result<User>> SignUp(User user);

}
