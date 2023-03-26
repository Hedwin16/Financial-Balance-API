using ApiRepository.Interfaces;
using DB.Models;
using DB.Models.Result;
using Microsoft.EntityFrameworkCore;

namespace ApiRepository.Services;

public class UserRepository : IUserRepository
{
    private readonly ApiContext context;

    public UserRepository(ApiContext context)
    {
        this.context = context;
    }

    public Result<bool> DeleteUser(User user)
    {
        throw new NotImplementedException();
    }

    public Result<List<User>> Find(Func<User, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<List<User>>> GetAll()
    {
        //var toke = new CancellationTokenRegistration();
        //var token = toke.Token;
        try
        {
            var list = new List<User>();

            list = await context.Users.ToListAsync();

            //await Task.Run(() => {
            //    list = context.Users.ToList();
            //}, token);

            return new SuccessResult<List<User>>(list);
        }
        catch (Exception ex)
        {
            //toke.Dispose();
            return new ErrorResult<List<User>>("Error: " + ex.ToString());
        }
    }

    public Result<User> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Result<User> GetByName(User user)
    {
        throw new NotImplementedException();
    }

    public Result<User> Login(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<User>> SaveUser(User user, UserPrivilege userPrivilege)
    {
        try
        {
            if (user.Id <= 0)
            {
                var _user = context.Users.Add(user);

                if (_user is null)
                {
                    return new NotFoundResult<User>("No se ha podido resgistrar el usuario.");
                }
                context.SaveChanges();

                userPrivilege.IdUser = _user.Entity.Id;

                var _privilege = context.UserPrivileges.Add(userPrivilege);

                context.SaveChanges();

                return new SuccessResult<User>(_user.Entity);
            }

            // Modify
            var _userModified = context.Users.FirstOrDefault(y => y.Id == user.Id);

            if (_userModified is null)
            {
                return new NotFoundResult<User>("Ha ocurrido un error obteniendo datos.");
            }

            context.Attach(_userModified);

            _userModified.Name = user.Name;
            _userModified.Pass = user.Pass;

            if (userPrivilege != null)
            {
                var _privilege = context.UserPrivileges
                    .FirstOrDefault(p=> p.IdUser == _userModified.Id);

                _privilege.IdPrivilege = userPrivilege.IdPrivilege;

                context.Entry(_privilege)
                    .Property(x => x.IdPrivilege)
                    .IsModified = true;
            }

            context.Entry(_userModified)
                .Property(x => x.Name)
                .IsModified = true;

            context.Entry(_userModified)
                .Property(x => x.Pass)
                .IsModified = true;

            context.SaveChanges();

            return new SuccessResult<User>(user);
        }
        catch (Exception ex)
        {
            return new ErrorResult<User>($"Ha ocurrido un error obteniendo datos.");
        }
    }

    public Result<User> SignUp(User user)
    {
        throw new NotImplementedException();
    }
}
