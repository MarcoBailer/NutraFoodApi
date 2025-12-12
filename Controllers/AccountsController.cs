using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nutra.Models.Dtos.Registro;
using Nutra.Models.Usuario;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Nutra.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public AccountsController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModelDto newUser) // TODO: Create a separate DTO for registration
    {
        var userByName = await _userManager.FindByNameAsync(newUser.NomeCompleto);
        if (userByName != null)
        {
            return BadRequest("Username already exists.");
        }

        var userByEmail = await _userManager.FindByEmailAsync(newUser.Email);
        if (userByEmail != null)
        {
            return BadRequest("Email already registered.");
        }

        ApplicationUser user = new()
        {
            NomeCompleto = newUser.NomeCompleto,
            CPF = newUser.CPF,
            PerfilAtivo = new PerfilNutricional
            {
                // TODO: Inicializar PerfilNutricional ???
            }
        };

        var result = await _userManager.CreateAsync(user, newUser.PasswordHash);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);

            return BadRequest(new { message = errors });
        }

        return Ok(new { message = "User registered successfully." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModelDto loginModel)
    {
        var user = await _userManager.FindByEmailAsync(loginModel.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, loginModel.Password))
        {
            return Unauthorized("Invalid username or password.");
        }
        var token = GenerateJwtToken(user);
        return Ok(new { token });
    }

    private async Task<AuthResponseDto> GenerateJwtToken(ApplicationUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        bool canUseVoiceAssistant = userRoles.Contains("especial") || userRoles.Contains("admin") || userRoles.Contains("user");

        var authClaims = new List<Claim>
        {
           new Claim(ClaimTypes.Name, user.UserName),
           new Claim(ClaimTypes.Email, user.Email),
           new Claim("canUseVoiceAssistant", canUseVoiceAssistant.ToString().ToLower()),
           new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _configuration["JWT:Issuer"],
            Audience = _configuration["JWT:Audience"],
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(authClaims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new AuthResponseDto
        {
            Token = tokenHandler.WriteToken(token),
            Expiration = token.ValidTo
        };
    }
}
