using Microsoft.AspNetCore.Mvc;
using Nutra.Models.Dtos.Registro;
using Nutra.Models.Usuario;

namespace Nutra.Interfaces
{
    public interface IAccounts
    {
        Task<ApplicationUser> Register(RegisterModelDto newUser);
        Task<AuthResponseDto> Login (LoginModelDto loginModel);
    }
}
