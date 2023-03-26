using ApiRepository.Interfaces;
using DB.Models;
using Financial_Balance_API.Attributes;
using Financial_Balance_API.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static Financial_Balance_API.ApiSettings;

namespace Financial_Balance_API.Controllers.Users;

[ApiController]
public class UserController : Controller
{
    private readonly ILogger _logger;
    private readonly IUserRepository _repository;
    private readonly ApiSettings _apiSettings;

    public UserController(ILogger<UserController> logger,
       IUserRepository repository
       , IOptions<ApiSettings> options)
    {
        _logger = logger;
        _repository = repository;
        _apiSettings = options.Value;
    }

    [HttpPost("api/saveuser/{id_privilege}")]
    public async Task<IActionResult> SaveUser(int id_privilege, [FromBody] User user)
    {
        try
        {
            var priv = new UserPrivilege();
            priv.IdPrivilege = id_privilege;

            var result = await _repository.SaveUser(user, priv);

            if (!result.Success)
            {
                return Unauthorized(result.Message);
            }

            return Ok(result.Message);
        }
        catch (Exception ex)
        {
            return NotFound(ex.ToString());
        }
    }

    [HttpPost("api/users_get_all")]
    public async Task<IActionResult> GetAllUsers()
    {
        var TypeClient = ApiSession.GetCurrentKeyType(Request);

        if (TypeClient == KeyType.None)
        {
            Response.StatusCode = 404;
            return Unauthorized();
        }

        if (TypeClient == KeyType.Standard)
        {
            Response.StatusCode = 404;
            return Unauthorized();
        }

        var result = await _repository.GetAll();

        if (!result.Success)
        {
            return NotFound(result.Message);
        }

        return Ok(result.Data);
    }

    [AdminKey]
    [HttpPost("api/user_delete")]
    public async Task<IActionResult> DeleteUser([FromBody] User user)
    {
        var result = await _repository.DeleteUser(user);

        if (! result.Success)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Message);
    }

    [HttpPost("api/user_login")]
    public async Task<IActionResult> LoginUser([FromBody] User user)
    {
        var result = await _repository.Login(user);

        if (! result.Success)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Data);
    }

}
