using Nutra.Enum;
using Nutra.Models.Usuario;

namespace Nutra.Interfaces;

public interface ICalculadoraNutricional
{
    MetaNutricional GerarMetaInicial(PerfilNutricional perfil);
}
