using Nutra.Models;
using Nutra.Models.Dtos;

namespace Nutra.Interfaces
{
    public interface IUserProfile
    {
        Task<RetornoPadrao> PostPerfilNutricional(PerfilNutricionalDto perfilNutricional);
    }
}
