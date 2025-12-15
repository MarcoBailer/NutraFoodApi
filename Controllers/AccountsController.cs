using Microsoft.AspNetCore.Mvc;
using Nutra.Interfaces;
using Nutra.Models.Dtos.Registro;

namespace Nutra.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccounts _account;

    public AccountsController(IAccounts account)
    {
        _account = account;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModelDto newUser)
    {
        try
        {
            if (newUser == null)
                throw new ArgumentNullException(nameof(newUser), "O objeto newUser não pode ser nulo.");

            var user = await _account.Register(newUser);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModelDto loginModel)
    {
        try
        {
            if (loginModel == null)
                throw new ArgumentNullException(nameof(loginModel), "O objeto loginModel não pode ser nulo.");

            var login = await _account.Login(loginModel);
            return Ok(login);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
