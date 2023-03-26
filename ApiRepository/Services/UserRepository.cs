using ApiRepository.Interfaces;
using DB.Models;
using DB.Models.Result;
using Microsoft.EntityFrameworkCore;

namespace ApiRepository.Services;

public class UserRepository : IUserRepository
{
    private readonly ApiContext db;

    public UserRepository(ApiContext context)
    {
        this.db = context;
    }

    public async Task<Result<User>> SaveUser(User user, UserPrivilege userPrivilege)
    {
        try
        {
            if (user.Id <= 0)
            {
                var _user = db.Users.Add(user);

                if (_user is null)
                {
                    return new NotFoundResult<User>("No se ha podido resgistrar el usuario.");
                }
                db.SaveChanges();

                userPrivilege.IdUser = _user.Entity.Id;

                var _privilege = await db.UserPrivileges.AddAsync(userPrivilege);

                db.SaveChanges();

                return new SuccessResult<User>(_user.Entity, "Registrado con éxito");
            }

            // Modify
            var _userModified = await db.Users.FirstOrDefaultAsync(y => y.Id == user.Id);

            if (_userModified is null)
            {
                return new NotFoundResult<User>("Ha ocurrido un error obteniendo datos.");
            }

            db.Attach(_userModified);

            _userModified.Name = user.Name;
            _userModified.Pass = user.Pass;

            if (userPrivilege != null)
            {
                var _privilege = await db.UserPrivileges
                    .FirstOrDefaultAsync(p => p.IdUser == _userModified.Id);

                _privilege.IdPrivilege = userPrivilege.IdPrivilege;

                db.Entry(_privilege)
                    .Property(x => x.IdPrivilege)
                    .IsModified = true;
            }

            db.Entry(_userModified)
                .Property(x => x.Name)
                .IsModified = true;

            db.Entry(_userModified)
                .Property(x => x.Pass)
                .IsModified = true;

            db.SaveChanges();

            return new SuccessResult<User>(user, "Modificado con Éxito");
        }
        catch (Exception ex)
        {
            return new ErrorResult<User>($"Ha ocurrido un error obteniendo datos.");
        }
    }

    public async Task<Result<bool>> DeleteUser(User user)
    {
        try
        {
            if (user.Id <= 0)
            {
                return new NotFoundResult<bool>("Usuario no encontrado");
            }

            var _user = new User();

            _user = await db.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            if (_user is null)
            {
                return new NotFoundResult<bool>("El usuario a eliminar no existe.");
            }

            var priv = await db.UserPrivileges.FirstOrDefaultAsync(x => x.IdUser == user.Id);

            if (priv != null)
            {
                db.Remove(priv);
            }

            //db.UserPrivileges.RemoveRange(user.UserPrivileges);
            db.Users.Remove(_user);


            db.SaveChanges();

            return new SuccessResult<bool>(true, "Eliminado con Éxito");
        }
        catch (Exception)
        {
            return new ErrorResult<bool>("Ha ocurrido un error intentando eliminar el usuario.");
        }
    }

    public async Task<Result<User>> SignUp(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<List<User>>> Find(Func<User, bool> predicate)
    {
        try
        {
            IEnumerable<User> users = new List<User>();  

            await Task.Run(() =>
            {
                users = db.Users.Where(predicate);
            });

            if (users is null)
            {
                return new NotFoundResult<List<User>>("No se han encontrado coincidencias");
            }

            return new SuccessResult<List<User>>(users.ToList());
        }
        catch (Exception ex)
        {
            return new ErrorResult<List<User>>("Ha ocurrido un error");
        }
    }

    public async Task<Result<List<User>>> GetAll()
    {
        //var toke = new CancellationTokenRegistration();
        //var token = toke.Token;
        try
        {
            var list = new List<User>();

            list = await db.Users.ToListAsync();

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

    public async Task<Result<User>> GetById(int id)
    {
        try
        {
            var user = await db.Users.FindAsync(id);

            if (user is null)
            {
                return new ErrorResult<User>("Not Found");
            }

            return new SuccessResult<User>(user);

        }
        catch (Exception ex)
        {
            return new ErrorResult<User>("Error: " + ex.ToString());
        }
    }

    public Task<Result<User>> GetByName(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<User>> Login(User user)
    {
        try
        {
            var _user = await db.Users.FirstOrDefaultAsync(
                    x => x.Name == user.Name
                    && x.Pass == user.Pass
                    //&& x.Status == 1
                );

            if (_user is null)
            {
                return new NotFoundResult<User>("Datos incorrectos");
            }

            return new SuccessResult<User>(_user, "Loggeado con Éxito");
        }
        catch (Exception)
        {
            return new ErrorResult<User>("Ha ocurrido un error");
        }
    }

}
