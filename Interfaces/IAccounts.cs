using Nutra.Models;
using Nutra.Models.Dtos.Registro;

namespace Nutra.Interfaces
{
    public interface IAccounts
    {
        Task<RetornoPadrao> Register(RegisterModelDto newUser);
        Task<AuthResponseDto> Login (LoginModelDto loginModel);
    }
}
