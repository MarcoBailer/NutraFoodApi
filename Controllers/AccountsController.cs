using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nutra.Interfaces;
using Nutra.Models.Dtos;
using Nutra.Models.Dtos.Registro;
using Nutra.Models.Usuario;
using System.Security.Claims;

namespace Nutra.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountsController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    /// <summary>
    /// Front-end uses a token generated externally to see the user's profile information stored locally.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> containing the user's profile information if found; otherwise, a NotFound result
    /// if the user does not exist.</returns>
    [HttpGet("me")]
    public async Task<IActionResult> GetMyProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null) return NotFound("Usuário não encontrado no contexto local.");

        return Ok(new
        {
            user.NomeCompleto,
            user.Email,
            user.CPF,
        });
    }


    /// <summary>
    /// Update only profile data managed by Projeto B
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("update-profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null) return NotFound();

        // Atualiza apenas dados que são responsabilidade do Projeto B
        user.CPF = model.Cpf;
        user.NomeCompleto = model.NomeCompleto;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded) return Ok(user);

        return BadRequest(result.Errors);
    }
}
