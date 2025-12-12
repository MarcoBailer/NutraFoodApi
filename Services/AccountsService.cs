using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nutra.Interfaces;
using Nutra.Models.Dtos.Registro;
using Nutra.Models.Usuario;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Nutra.Services
{
    public class AccountsService : IAccounts
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;


        public AccountsService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<ApplicationUser> Register(RegisterModelDto newUser)
        {
            try
            {
                if (newUser == null)
                {
                    throw new ArgumentNullException(nameof(newUser), "O objeto newUser não pode ser nulo.");
                }

                var userByEmail = _userManager.FindByEmailAsync(newUser.Email);
                if (userByEmail.Result != null)
                {
                    throw new InvalidOperationException("Email já cadastrado.");
                }
                var userByUsername = _userManager.FindByNameAsync(newUser.Username);
                if (userByUsername.Result != null)
                {
                    throw new InvalidOperationException("Nome de usuário já cadastrado.");
                }

                var user = new ApplicationUser
                {
                    UserName = newUser.Username,
                    Email = newUser.Email,
                    NomeCompleto = newUser.NomeCompleto,
                    CPF = newUser.CPF,
                    PerfilAtivo = null
                };

                var result = await _userManager.CreateAsync(user, newUser.PasswordHash);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description);
                    throw new InvalidOperationException(string.Join("; ", errors));
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao registrar o usuário: " + ex.Message);
            }
        }

        public async Task<AuthResponseDto> Login (LoginModelDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if(user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
            {
                throw new UnauthorizedAccessException("Usuário ou senha inválidos.");
            }
            var token = await GenerateJwtToken(user);
            return token;
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
}
