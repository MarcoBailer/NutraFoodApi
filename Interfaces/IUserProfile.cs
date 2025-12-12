using Nutra.Models.Usuario;

namespace Nutra.Interfaces
{
    public interface IUserProfile
    {
        Task<PerfilNutricional> PostPerfilNutricional(PerfilNutricional perfilNutricional);
    }
}
