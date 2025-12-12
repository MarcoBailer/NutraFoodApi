using Microsoft.AspNetCore.Mvc;
using Nutra.Interfaces;
using Nutra.Models.Usuario;

namespace Nutra.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserProfile _userProfile;

    public UserController(IUserProfile userProfile)
    {
        _userProfile = userProfile;
    }

    [HttpPost("perfil-nutricional")]
    public async Task<IActionResult> PostPerfilNutricional([FromBody] PerfilNutricional perfilNutricional)
    {
        try
        {
            var perfil = await _userProfile.PostPerfilNutricional(perfilNutricional);
            return Ok(perfil);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
