using Microsoft.AspNetCore.Mvc;
using Nutra.Models;
using Nutra.Models.Dtos.Registro;
using Nutra.Models.Usuario;

namespace Nutra.Interfaces
{
    public interface IAccounts
    {
        Task<RetornoPadrao> Register(RegisterModelDto newUser);
        Task<AuthResponseDto> Login (LoginModelDto loginModel);
    }
}
